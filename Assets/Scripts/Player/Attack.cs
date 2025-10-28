using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class Attack : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float attackCooldown;
    private float attackSpeed = 1f;
    [SerializeField] private float parryCooldown;
    private Player playerScript;

    public float playerDamage;
    [SerializeField] private float hit1Damage;
    [SerializeField] private float hit2Damage;
    [SerializeField] private float hit3Damage;
    private float criticalDamage;
    public bool canCrit;
    public float bleedingDamage;
    public bool canBleed;
    private float originalDamage;
    private Animator anim;
    private Transform player;
    public bool isAttacking = false;

    public bool colliderActive = false;
   
    public Vector3 knockbackDirection; 
    public Quaternion rotation;

    public Vector2 animDir;


    [SerializeField] private Transform attackCollider;

    private GameObject gameManager;


    private float originalAttackCooldown;
    private float originalAttackSpeed;

    public bool isSlowed = false;
    public float slowedSpeed;

    public float cooldownTime;
    private float nextFireTime = 0f;

    private float lastClickedTime = 0f;
    private int nOfClicks = 0;

    [SerializeField] private float maxComboDelay;

    public bool Hit3;

    public AudioSource hitnoise;





    //public bool attackSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform;
        playerScript = GetComponent<Player>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        anim.SetFloat("Attack Speed", attackSpeed);

        originalDamage = playerDamage;
        originalAttackCooldown = attackCooldown;
        originalAttackSpeed = attackSpeed;

       
       
    }

    void Update()
    {

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            nOfClicks = 0;
            anim.ResetTrigger("Hit2");
            anim.ResetTrigger("Hit3");
            //isAttacking = false;
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            CalculateParry();
        }

        if (Input.GetMouseButtonDown(0))
        {

            lastClickedTime = Time.time;
            nOfClicks++;
            nOfClicks = Mathf.Clamp(nOfClicks, 0, 3);

            if (Time.time > nextFireTime)
            {      
                if (!isAttacking)
                {
                    HandleAttack();
                }
                 
            }

        }

        if (isAttacking)
        {
            playerScript.speed = 0;
            playerScript.originalSpeed = 0;
        }

        else if (!isAttacking)
        {
            playerScript.speed = 25f;
            playerScript.originalSpeed = 25f;
        }


        ComboTransition1();
        ComboTransition2(); 
        attackCollider.gameObject.SetActive(colliderActive);

        


    }

    public void ClearAttackBoons()
    {
        playerDamage = originalDamage;
        attackCooldown = originalAttackCooldown;
        attackSpeed = originalAttackSpeed;
        anim.SetFloat("Attack Speed", attackSpeed);
        isSlowed = false;
    }

    public void ApplyAttackBoon(UpgradeScriptableObjects boon)
    {
        if (boon == null) return;

        if (boon.GetBoonName() == "Damage Increase")
        {
            playerDamage = playerDamage * (1 + boon.GetValue());

        }
        else if (boon.GetBoonName() == "Attack Cooldown")
        {
            attackCooldown = attackCooldown * (1 - boon.GetValue());
        }
        else if (boon.GetBoonName() == "Attack Speed")
        {
            attackSpeed = attackSpeed * (1 + boon.GetValue());
            anim.SetFloat("Attack Speed", attackSpeed);
        }
        else if (boon.GetBoonName() == "Slowness")
        {
            isSlowed = true;
            slowedSpeed = boon.GetValue();
        }

        else if (boon.GetBoonName() == "Critical Damage")
        {
            canCrit = true;
            criticalDamage = hit3Damage * (1 + boon.GetValue());
        }

        else if (boon.GetBoonName() == "Bleed")
        {
            canBleed = true;
            bleedingDamage = playerDamage * (boon.GetValue());
        }
    }
  
    void CalculateParry()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 10000f))
        {
            Vector3 targetPoint = hit.point;
            Vector3 dir = (targetPoint - player.position);
            dir.y = 0;
            dir.Normalize();

            knockbackDirection = dir.normalized;

            if (attackCollider != null)
            {
                attackCollider.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }

            Vector2 dir2D = new Vector2(dir.x, dir.z);
            float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;
            angle -= 90f;
            if (angle < 0) angle += 360;

            animDir = GetDirection(angle);
            anim.SetFloat("AttackHorizontal", animDir.x);
            anim.SetFloat("AttackVertical", animDir.y);

            playerScript.lastMovement.x = animDir.x;
            playerScript.lastMovement.z = animDir.y;         
        }

    }
    void HandleAttack()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //ray.origin = cam.transform.position + cam.transform.forward * 0.1f;
        if (Physics.Raycast(ray, out RaycastHit hit, 10000f))
        {
            Vector3 targetPoint = hit.point;
            Vector3 dir = (targetPoint - player.position);
            dir.y = 0;
            dir.Normalize();

            knockbackDirection = dir.normalized;

            if (attackCollider != null)
            {

                attackCollider.rotation = Quaternion.LookRotation(dir, Vector3.up);  
            }

            Vector2 dir2D = new Vector2(dir.x, dir.z);
            float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;
            angle -= 90f;
            if (angle < 0) angle += 360;

            animDir = GetDirection(angle);
            anim.SetFloat("AttackHorizontal", animDir.x);
            anim.SetFloat("AttackVertical", animDir.y);
            
            if (nOfClicks == 1)
            {
                maxComboDelay = 1;
                RandomPitchAttack();
                playerDamage = hit1Damage;
                anim.SetTrigger("Hit1");
            }

          

            playerScript.lastMovement.x = animDir.x;
            playerScript.lastMovement.z = animDir.y;
            isAttacking = true;
            StartCoroutine(CanAttack());
        }
    }
   
    void RandomPitchAttack()
    {
        hitnoise.pitch = Random.Range(1.2f, 1.3f);
        hitnoise.Play();
    }

     public void ComboTransition1()
{
    if (nOfClicks == 2)
    { 
            HandleAttack();
        RandomPitchAttack();
        playerDamage = hit2Damage;
     
        anim.SetTrigger("Hit2");
    }
    else
    {
        isAttacking = false;
    }
}

public void ComboTransition2()
{
    if (nOfClicks == 3)
    {
        RandomPitchAttack();

        if (!canCrit)
            {
                playerDamage = hit3Damage;
            }
       
        if (canCrit)
            {
                playerDamage = criticalDamage;
            }

        HandleAttack();
        anim.SetTrigger("Hit3");
        maxComboDelay = 0.3f;
        nOfClicks = 0;
     }
        else
        {
            StartCoroutine(resetSpeed());
        }
       
    }


    IEnumerator resetSpeed()
    {
        yield return new WaitForSeconds(4);
            isAttacking = false;
        
    }
    Vector2 GetDirection(float angle)
    {
      
        if (angle >= 337.5f || angle < 22.5f) return new Vector2(0,1);//North
        if (angle >= 22.5f && angle < 67.5f) return new Vector2(-1,1); //North West
        if (angle >= 67.5f && angle < 112.5f) return new Vector2(-1,0); //West
        if (angle >= 112.5f && angle < 157.5f) return new Vector2(-1, -1); //South West
        if (angle >= 157.5f && angle < 202.5f) return new Vector2(0, -1); //South
        if (angle >= 202.5f && angle < 247.5f) return new Vector2(1,-1); //South East
        if (angle >= 247.5f && angle < 292.5f) return new Vector2(1,0); //East
        if (angle >= 292.5f && angle < 337.5f) return new Vector2(1, 1); //North East

      

        return Vector2.zero;
    }

    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        nextFireTime = Time.time + attackCooldown;
    }


}


