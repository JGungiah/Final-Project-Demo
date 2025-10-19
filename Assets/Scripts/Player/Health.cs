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
    public Image shieldSlider;
    public Image brokenShield;
    public bool canTakeDamage;

    public float currentHealth;
    

    public bool hasBeenAttacked = false;
    [SerializeField] private float hitStopDuration;
    private CameraFollow cameraScript;

    public bool canBlock;
    public bool isBlocking = false;
    [SerializeField] private float blockStrength;
    [SerializeField] private float blockRegenSpeed;
    private bool blockCoolDown;
    public AudioSource block;


    private AttackMelee enemyAttack;

    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;
    [SerializeField] private float stunDuration;

    private Player movementScript;
    private float originalDamage;
    [SerializeField] private float EnemyDamage;
    [SerializeField] private float projectileDamage;

    private Animator anim;

    private GameObject gameManager;

    public float originalMaxHealth;
    public float originalKnockBackPower;
    public float originalBlockStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        
        currentHealth = maxHealth;
        movementScript = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cameraScript = FindAnyObjectByType<CameraFollow>();
        originalDamage = EnemyDamage;
        canBlock = true;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        
        originalMaxHealth = maxHealth;
        originalBlockStrength = blockStrength;
        originalKnockBackPower = knockbackPower;

        
    }

    // Update is called once per frame
    void Update()
    {
        shieldSlider.fillAmount = blockStrength / originalBlockStrength;
        Block();
        StartCoroutine(BlockRegen());
        healthBar.fillAmount = currentHealth / maxHealth;

        if (!canBlock)
        {
            brokenShield.gameObject.SetActive(true);
        }
        else
        {
            brokenShield.gameObject.SetActive(false);
        }

        if (currentHealth <= 0 )
        {
            SceneManager.LoadScene("LobbyRoom");
            currentHealth = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canBlock)
        {
            if (isBlocking)
            {
                block.pitch = Random.Range(1.5f, 1.6f);
                block.Play();
            }
        }
        
    }

    public void ClearHealthBoons()
    {
        maxHealth = originalMaxHealth;
        currentHealth = maxHealth;
        knockbackPower = originalKnockBackPower;
        blockStrength = originalBlockStrength;
    }

    

    public void ApplyHealthBoon(UpgradeScriptableObjects boon)
    {
        if (boon == null) return;

        if (boon.GetBoonName() == "Health Increase")
        {
            maxHealth = maxHealth * (1 + boon.GetValue());
            currentHealth = maxHealth;
        }
        else if (boon.GetBoonName() == "Parry Window")
        {
           blockStrength = blockStrength * (1 + boon.GetValue());
        }
        else if (boon.GetBoonName() == "Parry Knockback")
        {
            knockbackPower = knockbackPower * (1 + boon.GetValue());
        }
    }


    public void TakeDamage (float damage)
    {   
        currentHealth -= damage ;
    }

    void Block()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canBlock)
        {
            anim.SetBool("isBlocking", true);
            isBlocking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            anim.SetBool("isBlocking", false);
            isBlocking = false;
            
        }

        if (blockStrength <= 0)
        {
            anim.SetBool("isBlocking", false);
            canBlock = false;
            isBlocking = false;
            blockCoolDown = true;
            BlockRegen();
        }    

        if (blockStrength == originalBlockStrength)
        {
            canBlock = true;
        }

        if (isBlocking && canBlock)
        {
            movementScript.speed = 0;
        }
        else
        {
            movementScript.speed = 25f;
        }
    }

    IEnumerator BlockRegen()
    {
        if (!isBlocking)
        {
            yield return new WaitForSeconds(2f);
            blockStrength += blockRegenSpeed * Time.deltaTime;
            blockStrength = Mathf.Clamp(blockStrength, 0, originalBlockStrength);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("JotunnCollider"))
        {
            enemyAttack = other.GetComponentInParent<AttackMelee>();

            if (enemyAttack != null && enemyAttack.hasAttacked && !hasBeenAttacked)
            {     

                if (!isBlocking)
                {
                    TakeDamage(enemyAttack.damage);
                    hasBeenAttacked = true;
                    StartCoroutine(AttackWindow());
                }
                else if (isBlocking)
                {
                    blockStrength -= enemyAttack.damage / 4;
                    blockStrength = Mathf.Clamp(blockStrength, 0 , originalBlockStrength);
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
        if (other.tag == "Poison")
        {
            canTakeDamage = true;
            StartCoroutine(TakeWorldDamage(5f));
        }
        if (other.tag == "Rock")
        {
            TakeDamage(20f);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Poison")
        {
            canTakeDamage = false;
          
        }
    }

    public IEnumerator TakeWorldDamage(float damagePerSecond)
    {
        while (canTakeDamage)
        {
          currentHealth -= damagePerSecond;
            yield return new WaitForSeconds(0.7f);
           
        }
    }

   
    IEnumerator AttackWindow()
    {
        yield return new WaitForSeconds(0.1f);
        hasBeenAttacked = false ;
    }
}
