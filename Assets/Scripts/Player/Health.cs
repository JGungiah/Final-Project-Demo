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
     private float stunnedDamage;
    private Animator anim;
    private bool attemptedParry = false;
    public GameObject parryVFX;

    private Color parryColor = Color.green;
    private Color missParryColor = Color.red;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
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
    
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0 )
        {
            SceneManager.LoadScene("LobbyRoom");
            currentHealth = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canParry)
        {
            StartCoroutine(CheckParry());
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
                   StartCoroutine(ParryKnockBack());
                   StartCoroutine(ParryColor());

                }
                cameraScript.Shake();   
              
                enemyAttack.hasAttacked = false;
            }
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
