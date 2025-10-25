using UnityEngine;
using System.Collections;

public class Yggdrasil : MonoBehaviour
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
    public bool isHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //originalColor = spriteRenderer.color;
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<Attack>();
       
    }

    // Update is called once per frame
    void Update()
    {

      
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable)
        {
            print(1);
            currentHealth -= playerAttack.playerDamage;
            canTakeDamage = true;
            bloodVFX.SetActive(true);
            //StartCoroutine(HitColour());
            StartCoroutine(DamageWindow());


        }
    }

    IEnumerator DamageWindow()
    {
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = false;
        bloodVFX.SetActive(false);

    }

    //IEnumerator HitColour()
    //{

    //    //spriteRenderer.color = enemyHitColour;
    //    isHit = true;
    //    yield return new WaitForSeconds(hitDuration);
    //    //spriteRenderer.color = originalColor;

    //}


}
