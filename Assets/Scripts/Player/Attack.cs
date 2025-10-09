using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float attackCooldown;
    private float attackSpeed = 1f;
    [SerializeField] private float parryCooldown;
    private Player playerScript;

    public float playerDamage;
    private float originalDamage;
    private Animator anim;
    private Transform player;
    public bool isAttacking = false;
    public bool isParrying = false;
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

    //public float cooldownTime;
    private float nextFireTime = 0f;
    private static int nOfClicks = 0;
    private float lastClickedTime = 0f;
    private float maxComboDelay = 0.3f;

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
        ComboCheck();   

        if (Input.GetMouseButtonDown(1))
        {
            CalculateParry();
        }

        if (Input.GetMouseButtonDown(0) /*&& !isAttacking*/)
        {
            if (Time.time > nextFireTime)
            {
               
                HandleAttack();

            }
            
        }

        if (isAttacking)
        {
            playerScript.originalSpeed = 0;
        }

        else if (!isAttacking)
        {
            playerScript.originalSpeed = 25;
        }

        if (isParrying)
        {
            playerScript.speed = 0;
        }
        else if (!isParrying)
        {
            playerScript.speed = playerScript.originalSpeed;
        }

       

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
            playerDamage = playerDamage * (1 - boon.GetValue());

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
    }
  
    void CalculateParry()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
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

            isParrying = true;
            StartCoroutine(CanParry());
        }

    }
    void HandleAttack()
    {
        lastClickedTime = Time.time;
        nOfClicks++;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
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


            AttackCombo();
          

            

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

    void AttackCombo()
    {
        if (nOfClicks == 1)
        {
            RandomPitchAttack();
            anim.SetTrigger("Hit1");

     
            Hit3 = false;
        }
        else if (nOfClicks == 2)
        {
            RandomPitchAttack();
            anim.SetTrigger("Hit2");

            Hit3 = false;
        }
        else if (nOfClicks == 3)
        {
            RandomPitchAttack();
            anim.SetTrigger("Hit3");


        }

        nOfClicks = Mathf.Clamp(nOfClicks, 0, 3);
    }

    void ComboCheck()
    {
        var state = anim.GetCurrentAnimatorStateInfo(0);

        if (isAttacking && !state.IsName("Hit1") && !state.IsName("Hit2") && !state.IsName("Hit3") && Time.time - lastClickedTime > maxComboDelay)
        {
            isAttacking = false;
            nOfClicks = 0;
            Hit3 = false;
            
        }



        if (Time.time - lastClickedTime > maxComboDelay)
        {
            nOfClicks = 0;
        }
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



    IEnumerator CanParry()
    {

        yield return new WaitForSeconds(parryCooldown);
        isParrying = false;
    }

}


