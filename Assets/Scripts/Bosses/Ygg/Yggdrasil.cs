using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Yggdrasil : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;
    public float FollowHealth;
    private YggdrasilAttack Yggattack;

    private GameObject player;
    private Attack playerAttack;
    private Health HealthScript;
    public bool canTakeDamage = false;
    public bool isInvunrable = false;

    public GameObject bloodVFX;
 
    [SerializeField] private float hitDuration;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public bool isHit = false;
    public Animator healthShake;
    public Image healthbar;

    [Header("RootAttack")]
    public bool rootshathattacked;
    public bool rootshathattacked2;
    public bool rootshathattacked3;
    public float rootDamageThreshold;
    public float rootDamageThreshold2;
    public float rootDamageThreshold3;
    [Header("RockAttack")]
    public bool rockshathfallen;
    public bool rockshathfallen2;
    public bool rockshathfallen3;
    public float rockDamageThreshold;
    public float rockDamageThreshold2;
    public float rockDamageThreshold3;

    [Header("CloseRangeAttack")]
    public bool ishighhealth;
    public bool isMidhealth;
    public bool isLowhealth;
    public float swipeDamageThreshold;
    public float swipeDamageThreshold2;
    public float swipeDamageThreshold3;
    public bool isIncollider;

    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;

    private CharacterController characterController;

    public AudioSource hitNoise;
    public AudioSource impactSound;

    public GameObject[] deathVFX;
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
        FollowHealth = currentHealth;
    }

    public void Continue()
    {
        SceneManager.LoadScene("Outro");
    }

    // Update is called once per frame
    void Update()
    {
        health();
      Rockstarters();
        Rootstarters();
        Swipe();

        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }

    }

    IEnumerator Death()
    {
        deathVFX[0].SetActive(true);

        yield return new WaitForSeconds(3);

        deathVFX[1].SetActive(true);


        yield return new WaitForSeconds(1.5f);

        deathVFX[2].SetActive(true);


        yield return new WaitForSeconds(1.5f);

        deathVFX[3].SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage && !isInvunrable)
        {
            hitNoise.pitch = Random.Range (0.8f, 1.1f);
            hitNoise.PlayOneShot(hitNoise.clip);
            healthShake.SetTrigger("isHit");
            currentHealth -= playerAttack.playerDamage;
          
            canTakeDamage = true;
            bloodVFX.SetActive(true);
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
        if (currentHealth <= rockDamageThreshold2 && !rockshathfallen2)
        {
            isInvunrable = true;
            Yggattack.StartCoroutine(Yggattack.StartFalling());
            rockshathfallen2 = true;
        }
        if (currentHealth <= rockDamageThreshold3 && !rockshathfallen3)
        {
            isInvunrable = true;
            Yggattack.StartCoroutine(Yggattack.StartFalling());
            rockshathfallen3 = true;
        }
    }
    public void Rootstarters()
    {
        if (currentHealth <= rootDamageThreshold && !rootshathattacked)
        {
            isInvunrable = true;
            //KnockBack();
        
            Yggattack.StartCoroutine(Yggattack.RootAttack());
            rootshathattacked = true;
           
        }
        if (currentHealth <= rootDamageThreshold2 && !rootshathattacked2)
        {
            isInvunrable = true;
            Yggattack.StartCoroutine(Yggattack.RootAttack());
            rootshathattacked2 = true;
        }
        if (currentHealth <= rootDamageThreshold3 && !rootshathattacked3)
        {
            isInvunrable = true;
            Yggattack.StartCoroutine(Yggattack.RootAttack());
            rootshathattacked3 = true;
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
        if (currentHealth <= swipeDamageThreshold && isIncollider && !ishighhealth)
        {
            StartCoroutine(closeAttack());
            ishighhealth = true;
        }
        if (currentHealth <= swipeDamageThreshold2 && isIncollider && !isMidhealth)
        {
            StartCoroutine(closeAttack());
            isMidhealth = true;
        }
        if (currentHealth <= swipeDamageThreshold3 && isIncollider && !isLowhealth)
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

    public IEnumerator KnockBack()
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

        knockbackDir.y = 0f;
        knockbackDir.Normalize();



        Vector3 finalDir = knockbackDir;

        characterController.enabled = false;

        impactSound.Play();
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            player.transform.position += finalDir * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterController.enabled = true;


    }
    public void health()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }










    //IEnumerator HitColour()
    //{

    //    spriteRenderer.color = enemyHitColour;
    //    isHit = true;
    //    yield return new WaitForSeconds(hitDuration);
    //    spriteRenderer.color = originalColor;

    //}


}
