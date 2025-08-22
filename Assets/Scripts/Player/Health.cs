using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthDecreaseSpeed;
    [SerializeField] private Image healthBar;
    private float elapsedTime;
    public float currentHealth;
    private HitStop hitstopScript;
    private AttackMelee attackMeleeScript;

    public bool hasBeenAttacked = false;
    [SerializeField] private float hitStopDuration;
    private CameraFollow cameraScript;
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
               
               cameraScript.Shake();
               
                TakeDamage(10.0f);
                hasBeenAttacked = true;
                StartCoroutine(AttackWindow());


                enemyAttack.hasAttacked = false;
            }
        }
    }

    IEnumerator AttackWindow()
    {
        yield return new WaitForSeconds(0.1f);
        hasBeenAttacked = false ;
    }
}
