using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AttackMelee : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;

    private NavMeshAgent agent;


    [SerializeField] private float draugrDamage;
    [SerializeField] private float worshiperDamage;
    [SerializeField] private float jotunnDamage;
    [SerializeField] private float spiritDamage;

    private GameObject player;

    private Attack attackScript;
    private Animator anim;
    private EnemyMovement movementScript;
    public bool isBeingKnockedBack = false;

    public bool isAttacking = false;
    private bool canAttack = true;
    public bool hasAttacked = false;
    private bool canBeKnockedBack = true;

    [SerializeField] private Transform attackCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        attackScript = player.GetComponent<Attack>();
       
        movementScript = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!isAttacking)
        {
            attackCollider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && attackScript.isAttacking && !isBeingKnockedBack && canBeKnockedBack)
        {
            isBeingKnockedBack = true;
            canBeKnockedBack = false;
            StartCoroutine(KnockbackWindow());
            StartCoroutine(KnockBack());
            
           

        }
        else if (other.gameObject.CompareTag("PlayerCollider") && !isAttacking && canAttack)
        {
            StartCoroutine(EnemyAttack());

            if (isAttacking)
            {
                attackCollider.gameObject.SetActive(true);

                attackCollider.rotation = Quaternion.LookRotation(-movementScript.animDirection, Vector3.up);
            }
        }

    }

    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        canAttack = false;
       
        agent.enabled = false;

        anim.SetFloat("AttackHorizontal", movementScript.animDirection.x);
        anim.SetFloat("AttackVertical", movementScript.animDirection.z);
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(attackDuration);

        agent.enabled = true;
        isAttacking = false;
        attackCollider.gameObject.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator KnockBack()
    {   
        agent.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            transform.position += attackScript.knockbackDirection * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        agent.enabled = true;
        isBeingKnockedBack = false;
    }

    IEnumerator KnockbackWindow()
    {
        yield return new WaitForSeconds(1);
        canBeKnockedBack = true;
    }
}
