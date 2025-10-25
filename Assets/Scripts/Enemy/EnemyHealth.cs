using System.Collections;
using TMPro;
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

    private float randomValue;
    public TMP_Text floatingText;

    [SerializeField] private Material dissolveMat;
    private Shader shader;
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
        floatingText.text = playerAttack.playerDamage.ToString();
        if (currentHealth <= 0 && !hasDropped)
        {
            EnemyDrop();
            hasDropped = true;
            //dissolveMat = new Material(dissolveMat);
            //spriteRenderer.material = dissolveMat;
            //StartCoroutine(DissolveEffect());
        }

        Destroy(this.gameObject, 0.1f);






    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable)
        {
            
            randomValue = Random.Range(transform.position.x - 1, transform.position.x + 1);
            Instantiate(floatingText, new Vector3(randomValue, transform.position.y - 5, transform.position.z - 2), Quaternion.identity);
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

    //private IEnumerator DissolveEffect()
    //{
    //    float dissolveTime = 2f;
    //    float elapsedTime = 0f;
    //    float startValue = 1f; 
    //    float endValue = 0f;   

    //    string dissolveProperty = "_DissolveAmount";

    //    while (elapsedTime < dissolveTime)
    //    {
    //        float dissolveValue = Mathf.Lerp(startValue, endValue, elapsedTime / dissolveTime);
    //        dissolveMat.SetFloat(dissolveProperty, dissolveValue);

    //        elapsedTime += Time.deltaTime;
    //        yield return null; 
    //    }

    //    dissolveMat.SetFloat(dissolveProperty, endValue);

    //    Destroy(gameObject);
    //}
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
