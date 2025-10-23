using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    
    [SerializeField] private float maxHealth;
    public float currentHealth;
    private float bleedDamage;

    private GameObject player;
    private Attack playerAttack;
    public bool canTakeDamage = false;
    public bool isInvunrable = false;

    public GameObject bloodVFX;
    private Color enemyHitColour = Color.red;
    [SerializeField] private float hitDuration;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent agent;

    private float originalSpeed;
    private bool slowed = false;

    public GameObject enemyDrop;
    private bool hasDropped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<Attack>();
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = agent.speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            if (!hasDropped)
            {
                EnemyDrop();
                hasDropped = true;
            }
        
            Destroy(this.gameObject, 0.1f);
        }

       


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable)
        {
            currentHealth -= playerAttack.playerDamage;
            currentHealth = Mathf.Clamp(currentHealth, 0 , maxHealth);
            canTakeDamage = true;
            
            bloodVFX.SetActive(true);
            StartCoroutine(HitColour());
            StartCoroutine(DamageWindow());

            if (playerAttack.isSlowed && !slowed)
            {
                agent.speed = agent.speed * (1 - playerAttack.slowedSpeed); 
                slowed = true;
                StartCoroutine(ResetSpeed());
            }

            if (playerAttack.canBleed)
            {
                StartCoroutine(BleedEffect());
            }
        }
    }
    IEnumerator BleedEffect()
    {
        float bleedDuration = 5f;  
        float tickInterval = 1f;   
        float elapsed = 0f;

        bleedDamage = playerAttack.bleedingDamage; 

        while (elapsed < bleedDuration)
        {
            currentHealth -= bleedDamage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            bloodVFX.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            bloodVFX.SetActive(false);

            yield return new WaitForSeconds(tickInterval);

            elapsed += tickInterval;
        }

    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(5f);
        agent.speed = originalSpeed;
        slowed = false;
    }

    IEnumerator DamageWindow()
    {
        yield return new WaitForSeconds(0.1f);
        canTakeDamage = false;
        if (!playerAttack.canBleed)
        {
            bloodVFX.SetActive(false);
        }
        
    }

    IEnumerator HitColour()
    {
      
        spriteRenderer.color = enemyHitColour;
        yield return new WaitForSeconds(hitDuration);
        spriteRenderer.color = originalColor;
      
    }

    public void EnemyDrop()
    {
        Instantiate(enemyDrop, this.transform.position, Quaternion.identity);   
    }
    
}
