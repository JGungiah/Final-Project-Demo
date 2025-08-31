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
    private float elapsedTime;
    public float currentHealth;
    private HitStop hitstopScript;
    private AttackMelee attackMeleeScript;

    public bool hasBeenAttacked = false;
    [SerializeField] private float hitStopDuration;
    private CameraFollow cameraScript;

    [SerializeField] private bool isParrying = false;
    [SerializeField] private float parryDuration;
    [SerializeField] private bool canParry = true;
    [SerializeField] private float parryCoolDown;

    private AttackMelee enemyAttack;
    private EnemyMovement enemyMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        currentHealth = maxHealth;
        hitstopScript = GetComponent<HitStop>();
        cameraScript = FindAnyObjectByType<CameraFollow>();
    
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0 )
        {
            SceneManager.LoadScene("LobbyRoom");
            currentHealth = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canParry)
        {
            StartCoroutine(CheckParry());
        }
    }

    public void TakeDamage (float damage)
    {
       
        elapsedTime += Time.deltaTime;
        currentHealth -= damage ;
 

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("JotunnCollider"))
        {
            enemyAttack = other.GetComponentInParent<AttackMelee>();
            enemyMovement = other.GetComponentInParent<EnemyMovement>();

            if (enemyAttack != null && enemyAttack.hasAttacked && !hasBeenAttacked)
            {

                if (!isParrying)
                {
                    TakeDamage(5);
                    hasBeenAttacked = true;
                    StartCoroutine(AttackWindow());
                }

                if (isParrying)
                {
                    float elapsedTime = 0f;
                    while (elapsedTime < 0.5f)
                    {
                        enemyMovement.transform.position += -enemyMovement.animDirection * 2 * Time.deltaTime;
                        elapsedTime += Time.deltaTime;
                    }
                    


                }
                cameraScript.Shake();   
              

                enemyAttack.hasAttacked = false;
            }
        }
    }

   

    IEnumerator CheckParry()
    {
        isParrying = true;
        canParry = false;
        yield return new WaitForSeconds(parryDuration);
        
        isParrying = false;

        yield return new WaitForSeconds(parryCoolDown);
        canParry = true;
    }

    IEnumerator AttackWindow()
    {
        yield return new WaitForSeconds(0.1f);
        hasBeenAttacked = false ;
    }
}
