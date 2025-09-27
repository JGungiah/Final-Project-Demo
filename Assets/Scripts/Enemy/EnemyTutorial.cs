using UnityEngine;
using System.Collections;

public class EnemyTutorial : MonoBehaviour
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

    private float horizontalMovement;
    private float verticalMovement;
    private Animator anim;
    public Vector3 animDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<Attack>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        EnemyAnimations();

    }
    void EnemyAnimations()
    {

        animDirection = (player.transform.position - transform.position).normalized;
        animDirection.y = 0;

        horizontalMovement = animDirection.x;
        verticalMovement = animDirection.z;

        anim.SetFloat("horizontalMovement", horizontalMovement);
        anim.SetFloat("verticalMovement", verticalMovement);
        anim.SetFloat("animMoveMagnitude", animDirection.magnitude);


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable)
        {
            currentHealth -= playerAttack.playerDamage;
            canTakeDamage = true;
            bloodVFX.SetActive(true);
            StartCoroutine(HitColour());
            StartCoroutine(DamageWindow());


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
        yield return new WaitForSeconds(hitDuration);
        spriteRenderer.color = originalColor;

    }


}
