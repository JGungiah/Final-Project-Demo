using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Jormungandr : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    public float currentHealth;

    private Vector3 distToPlayer;

    private GameObject player;
    private Attack playerAttack;
    public bool canTakeDamage = false;
    public bool isInvunrable = false;

    public GameObject bloodVFX;
    private Color enemyHitColour = Color.red;
    [SerializeField] private float hitDuration;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public JormungandrAttack JormAttack;
    public GameObject Gate;
    private Animator anim;
    public Vector3 animDirection;
    public bool isHit = false;
    public Image healthbar;

    [Header("Rock Attacks")]
    public bool rockshathfallen;
    public bool rockshathfallen2;
    public bool rockshathfallen3;
    [SerializeField] private float rockDamageThreshold;
    [SerializeField] private float rockDamageThreshold2;
    [SerializeField] private float rockDamageThreshold3;

    [Header("Spit Attacks")]
    public bool spitshot;
    public PlayerTooFar playerToofar;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<Attack>();
        anim = GetComponent<Animator>();
        JormAttack = GetComponent<JormungandrAttack>();
        playerToofar = GetComponent<PlayerTooFar>();
    }

    private void Update()
    {
        health();
        Rockstarters();
        JormAttack.EnemySpawner();
        distToPlayer = transform.position - player.transform.position;
       if(currentHealth <= 0) 
        {
            Gate.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable /*&& damageRadius < distToPlayer.magnitude*/)
        {        
            currentHealth -= playerAttack.playerDamage;
            
            canTakeDamage = true;
            bloodVFX.SetActive(true);
            StartCoroutine(HitColour());
            StartCoroutine(DamageWindow());


        }
    }



    public void Rockstarters() 
    {
        if (currentHealth <= rockDamageThreshold && !rockshathfallen)
        {
            isInvunrable = true;
            JormAttack.StartCoroutine(JormAttack.StartFalling());
            rockshathfallen = true;
        }
        if (currentHealth <= rockDamageThreshold2 && !rockshathfallen2)
        {
            isInvunrable = true;
            JormAttack.StartCoroutine(JormAttack.StartFalling());
            rockshathfallen2 = true;
        }
        if (currentHealth <= rockDamageThreshold3 && !rockshathfallen3)
        {
            isInvunrable = true;
            JormAttack.StartCoroutine(JormAttack.StartFalling());
            rockshathfallen3 = true;
        }

    }

    IEnumerator DamageWindow()
    {
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = false;
        bloodVFX.SetActive(false);

    }

    IEnumerator HitColour()
    {

        spriteRenderer.color = enemyHitColour;
        isHit = true;
        yield return new WaitForSeconds(hitDuration);
        spriteRenderer.color = originalColor;

    }
    public void health() 
    {
        healthbar.fillAmount = currentHealth/ maxHealth;
    }


}
