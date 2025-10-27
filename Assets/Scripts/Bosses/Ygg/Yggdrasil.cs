using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Yggdrasil : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;

    private YggdrasilAttack Yggattack;

    private GameObject player;
    private Attack playerAttack;
    private Health HealthScript;
    public bool canTakeDamage = false;
    public bool isInvunrable = false;

    public GameObject bloodVFX;
    private Color enemyHitColour = Color.red;
    [SerializeField] private float hitDuration;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public bool isHit = false;


    [Header("RootAttack")]
    public bool rootshathattacked;
    public float rootDamageThreshold;


    [Header("RockAttack")]
    public bool rockshathfallen;
    public float rockDamageThreshold;

    [Header("CloseRangeAttack")]

    private bool isMidhealth;
    private bool isLowhealth;
    public bool isIncollider;

    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;

    private CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //originalColor = spriteRenderer.color;
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<Attack>();
        Yggattack = GetComponent<YggdrasilAttack>();
        HealthScript = player.GetComponent<Health>();
        characterController = player.GetComponent<CharacterController>();   

    }

    // Update is called once per frame
    void Update()
    {

      Rockstarters();
        Rootstarters();
        Swipe();
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

        if (other.gameObject.CompareTag("Player"))
        {
            isIncollider = true;

        }
    }

    public void Rockstarters()
    {
        if (currentHealth <= rockDamageThreshold && !rockshathfallen)
        {
            isInvunrable = true;
            Yggattack.StartCoroutine(Yggattack.StartFalling());
            rockshathfallen = true;
        }
    }
    public void Rootstarters()
    {
        if (currentHealth <= rootDamageThreshold && !rootshathattacked)
        {
            isInvunrable = true;
            Yggattack.StartCoroutine(Yggattack.RootAttack());
            rootshathattacked = true;
        }
    }
    IEnumerator DamageWindow()
    {
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = false;
        bloodVFX.SetActive(false);

    }
    public void Swipe()
    {
        if (currentHealth <= 700 && isIncollider && !isMidhealth)
        {
            StartCoroutine(closeAttack());
            isMidhealth = true;
        }
        if (currentHealth <= 400 && isIncollider && !isLowhealth)
        {
            StartCoroutine(closeAttack());
            isLowhealth = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIncollider = false;
        }
    }
    IEnumerator closeAttack()
    {
        while (isIncollider)
        {

            yield return new WaitForSeconds(3f);
            if (!isIncollider) yield break;
            HealthScript.currentHealth -= 20f;
            StartCoroutine(KnockBack());
            isIncollider = false;
        }
    }

    private IEnumerator KnockBack()
    {
        Vector3 flatDirection = (player.transform.position - transform.position);
        flatDirection.y = 0f;
        flatDirection.Normalize();

        float angle = Mathf.Atan2(flatDirection.x, flatDirection.z) * Mathf.Rad2Deg;

        Vector3 knockbackDir;

        if (angle >= -22.5f && angle < 22.5f)
        {
            knockbackDir = Vector3.forward; // North
        }
           
        else if (angle >= 22.5f && angle < 67.5f)
        {
            knockbackDir = (Vector3.forward + Vector3.right).normalized; // North-East
        }
           
        else if (angle >= 67.5f && angle < 112.5f)
        {
            knockbackDir = Vector3.right; // East
        }
           
        else if (angle >= 112.5f && angle < 157.5f)
        {
            knockbackDir = (Vector3.back + Vector3.right).normalized; // South-East
        }
           
        else if (angle >= 157.5f || angle < -157.5f)
        {
            knockbackDir = Vector3.back; // South
        }
           
        else if (angle >= -157.5f && angle < -112.5f)
        {
            knockbackDir = (Vector3.back + Vector3.left).normalized; // South-West
        }
            
        else if (angle >= -112.5f && angle < -67.5f)
        {
            knockbackDir = Vector3.left; // West
        }

        else
        {
            knockbackDir = (Vector3.forward + Vector3.left).normalized; // North-West
        }
           

        Vector3 finalDir = (knockbackDir + Vector3.down * 0.2f).normalized;

        characterController.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            player.transform.position += finalDir * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterController.enabled = true;


    }











    //IEnumerator HitColour()
    //{

    //    //spriteRenderer.color = enemyHitColour;
    //    isHit = true;
    //    yield return new WaitForSeconds(hitDuration);
    //    //spriteRenderer.color = originalColor;

    //}


}
