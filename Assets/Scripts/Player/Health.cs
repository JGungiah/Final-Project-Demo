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
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        currentHealth = maxHealth;
        hitstopScript = GetComponent<HitStop>();
        cameraScript = FindAnyObjectByType<CameraFollow>();
        //attackMeleeScript = FindAnyObjectByType<AttackMelee>();
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
        //attackMeleeScript.hasAttacked = false;
        //if (elapsedTime > healthDecreaseSpeed) ;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("JotunnCollider"))
        {
            AttackMelee enemyAttack = other.GetComponentInParent<AttackMelee>();

            if (enemyAttack != null && enemyAttack.hasAttacked && !hasBeenAttacked)
            {

                if (!isParrying)
                {
                    TakeDamage(5);
                    hasBeenAttacked = true;
                    StartCoroutine(AttackWindow());
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
