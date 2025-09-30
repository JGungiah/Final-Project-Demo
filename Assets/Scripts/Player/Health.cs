using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   
    [SerializeField] public float maxHealth;
    [SerializeField] private float healthDecreaseSpeed;
    [SerializeField] private Image healthBar;
   
    public float currentHealth;
    

    public bool hasBeenAttacked = false;
    [SerializeField] private float hitStopDuration;
    private CameraFollow cameraScript;

    [SerializeField] private bool isParrying = false;
    [SerializeField] private float parryDuration;
    [SerializeField] private bool canParry = true;
    [SerializeField] private float parryCoolDown;

    private AttackMelee enemyAttack;
    private EnemyMovement enemyMovement;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;
    [SerializeField] private float stunDuration;

    private Player movementScript;
    private float originalDamage;
    [SerializeField] private float EnemyDamage;
    [SerializeField] private float projectileDamage;
     private float stunnedDamage;
    private Animator anim;
    private bool attemptedParry = false;
    public GameObject parryVFX;

    private Color parryColor = Color.green;
    private Color missParryColor = Color.red;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    public AudioSource parrySuccesful;
    public AudioSource parryUnsuccesful;
    public AudioSource block;

    private GameObject gameManager;
    private RandomizeBoons randomizeBoons;
    private bool hasAppliedBoon = false;

    public float originalMaxHealth;
    public float originalParryDuration;
    public float originalKnockBackPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        
        currentHealth = maxHealth;
        movementScript = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cameraScript = FindAnyObjectByType<CameraFollow>();
        originalDamage = EnemyDamage;
        stunnedDamage = EnemyDamage * 2;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        randomizeBoons = gameManager.GetComponent<RandomizeBoons>();
        
        originalMaxHealth = maxHealth;
        originalParryDuration = parryDuration;
        originalKnockBackPower = knockbackPower;

        
    }

    // Update is called once per frame
    void Update()
    {

        //SelectedBoon();
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0 )
        {
            SceneManager.LoadScene("LobbyRoom");
            currentHealth = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canParry)
        {
            if (!isParrying)
            {
                block.pitch = Random.Range(1.5f, 1.6f);
                block.Play();
            }
          
            anim.SetTrigger("Parry");
            StartCoroutine(CheckParry());
        }

        
    }

    public void ClearHealthBoons()
    {
        maxHealth = originalMaxHealth;
        currentHealth = maxHealth;
        parryDuration = originalParryDuration;
        knockbackPower = originalKnockBackPower;
    }

    //void SelectedBoon()
    //{
    //        if (randomizeBoons.selectedBoon != null)
    //    {

    //            if (randomizeBoons.selectedBoon.GetBoonName() == "Health Increase" && !hasAppliedBoon)
    //            {
    //                print(1);
    //                maxHealth = maxHealth * (1 + randomizeBoons.selectedBoon.GetValue());
    //                currentHealth = maxHealth;
    //                hasAppliedBoon = true;
    //                StartCoroutine(ResetBoolCheck());

    //            }
    //            else if (randomizeBoons.selectedBoon.GetBoonName() == "Parry Window" && !hasAppliedBoon)
    //            {
    //                print(2);
    //                parryDuration = parryDuration * (1 + randomizeBoons.selectedBoon.GetValue());
    //                hasAppliedBoon = true;
    //                StartCoroutine(ResetBoolCheck());
    //            }
    //            else if (randomizeBoons.selectedBoon.GetBoonName() == "Parry Knockback" && !hasAppliedBoon)
    //            {
    //                print(3);
    //                knockbackPower = knockbackPower * (1 + randomizeBoons.selectedBoon.GetValue());
    //                hasAppliedBoon = true;
    //                StartCoroutine(ResetBoolCheck());
    //            }
    //        }
    //    }


    public void ApplyBoon(UpgradeScriptableObjects boon)
    {
        if (boon == null) return;

        if (boon.GetBoonName() == "Health Increase")
        {
            maxHealth = maxHealth * (1 + boon.GetValue());
            currentHealth = maxHealth;
        }
        else if (boon.GetBoonName() == "Parry Window")
        {
            parryDuration = parryDuration * (1 + boon.GetValue());
        }
        else if (boon.GetBoonName() == "Parry Knockback")
        {
            knockbackPower = knockbackPower * (1 + boon.GetValue());
        }
    }





    
   

    IEnumerator ParryColor()
    {
        spriteRenderer.color = parryColor;
        yield return new WaitForSeconds(parryDuration);
        spriteRenderer.color = originalColor;   
    }

    IEnumerator MissParryColor()
    {
        spriteRenderer.color = missParryColor;
        yield return new WaitForSeconds(parryDuration);
        spriteRenderer.color = originalColor;
    }
    public void TakeDamage (float damage)
    {
       
        currentHealth -= damage ;
 

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("JotunnCollider"))
        {
            enemyAttack = other.GetComponentInParent<AttackMelee>();
            enemyMovement = other.GetComponentInParent<EnemyMovement>();

            if (enemyAttack != null && enemyAttack.hasAttacked && !hasBeenAttacked)
            {
                if (attemptedParry && !isParrying)
                {
                    parryUnsuccesful.pitch = Random.Range(1.5f, 1.6f);
                    parryUnsuccesful.Play();
                    StartCoroutine(PunishPlayer());
                    StartCoroutine(MissParryColor());
                }

                if (!isParrying)
                {
                    TakeDamage(enemyAttack.damage);
                    hasBeenAttacked = true;
                    StartCoroutine(AttackWindow());
                }

                if (isParrying)
                {
                    parrySuccesful.pitch = Random.Range(1.5f, 1.6f);
                    parrySuccesful.Play();
                    StartCoroutine(ParryKnockBack());
                   StartCoroutine(ParryColor());

                }
                cameraScript.Shake();   
              
                enemyAttack.hasAttacked = false;
            }
        }

   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(projectileDamage);
            Destroy(other.gameObject);
        }
    }


    IEnumerator PunishPlayer()
    {
       movementScript.canMove = false;
       EnemyDamage = stunnedDamage;
       anim.enabled = false;
       yield return new WaitForSeconds(stunDuration);
       movementScript.canMove = true;
       EnemyDamage = originalDamage;
       anim.enabled = true;
    }
    IEnumerator ParryKnockBack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            enemyMovement.transform.position += -enemyMovement.animDirection * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }


    IEnumerator CheckParry()
    {
        attemptedParry = true;
        isParrying = true;
        canParry = false;
        parryVFX.SetActive(true);
        yield return new WaitForSeconds(parryDuration);
        
        isParrying = false;

        yield return new WaitForSeconds(parryCoolDown);
        canParry = true;
        attemptedParry = false;
        parryVFX.SetActive(false);
    }

    IEnumerator AttackWindow()
    {
        yield return new WaitForSeconds(0.1f);
        hasBeenAttacked = false ;
    }
}
