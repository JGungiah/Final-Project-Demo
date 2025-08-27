using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Attack attackScript;
    [SerializeField] private float maxHealth;
    public float currentHealth;
    [SerializeField]  private float playerDamage;
    private bool canTakeDamage = false;

    public GameObject bloodVFX;
    private Color enemyHitColour = Color.red;
    [SerializeField] private float hitDuration;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage)
        {
            currentHealth -= playerDamage;
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
