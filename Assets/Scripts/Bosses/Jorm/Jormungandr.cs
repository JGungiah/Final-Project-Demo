using System.Collections;
using UnityEngine;

public class Jormungandr : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    public float currentHealth;

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
   
    private Animator anim;
    public Vector3 animDirection;
    public bool isHit = false;


    [Header("Rock Attacks")]
    public bool rockshathfallen;

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
        Rockstarters();
        JormAttack.EnemySpawner();
      
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable)
        {
            print(1);
            currentHealth -= playerAttack.playerDamage;
            playerToofar.hasAttacked = true;
            canTakeDamage = true;
            bloodVFX.SetActive(true);
            StartCoroutine(HitColour());
            StartCoroutine(DamageWindow());


        }
    }

    public void Rockstarters() 
    {
        if (currentHealth <= 450 && !rockshathfallen)
        {
            isInvunrable = true;
            JormAttack.StartCoroutine(JormAttack.StartFalling());
            rockshathfallen = true;
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


}
