using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class AttackMelee : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;

    private NavMeshAgent agent;


    public float damage;

    private GameObject player;

    private Attack attackScript;
    private Animator anim;
    private EnemyMovement movementScript;
    public bool isBeingKnockedBack = false;

    public bool isAttacking = false;
    private bool canAttack = true;
    public bool hasAttacked = false;
    public bool canBeKnockedBack = true;

    [SerializeField] private Transform attackCollider;

    public float attackRadius;
    public float attackDistance;

    [SerializeField] private float hitSTopTime;

    public AudioSource attackSound;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;


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
            agent.isStopped = false;
        }

        attackDistance = Vector3.Distance(transform.position, player.transform.position);

        if (attackDistance <= attackRadius && !isAttacking && canAttack)
        {
            StartCoroutine(EnemyAttack());

            if (isAttacking)
            {
                attackCollider.gameObject.SetActive(true);

                attackCollider.rotation = Quaternion.LookRotation(-movementScript.animDirection, Vector3.up);
                agent.isStopped = true;
            }
        }

        if (anim.GetBool("attack"))
        {
            StartCoroutine(StopEnemy());
        }
    }

    private IEnumerator StopEnemy()
    {
        movementScript.canChase = false;
        agent.destination = transform.position;
        yield return new WaitForSeconds(5);
        movementScript.canChase = true;
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && attackScript.isAttacking && !isBeingKnockedBack && canBeKnockedBack)
        {
            isBeingKnockedBack = true;
            canBeKnockedBack = false;
            player.GetComponent <HitStop>().ApplyHitStop(hitSTopTime);
            StartCoroutine(KnockbackWindow());
            StartCoroutine(KnockBack());
        }
       
    }

    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        canAttack = false;


            attackSound.pitch = Random.Range(minPitch, maxPitch);
            attackSound.PlayOneShot(attackSound.clip);
        



        anim.SetFloat("AttackHorizontal", movementScript.animDirection.x);
        anim.SetFloat("AttackVertical", movementScript.animDirection.z);
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(attackDuration);


        isAttacking = false;
        attackCollider.gameObject.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        
    }

   public IEnumerator KnockBack()
    {   
        agent.isStopped = true;

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
           
           transform.position += attackScript.knockbackDirection * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        agent.isStopped = false;
        isBeingKnockedBack = false;
    }

    public IEnumerator KnockbackWindow()
    {
        yield return new WaitForSeconds(0);
        canBeKnockedBack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
