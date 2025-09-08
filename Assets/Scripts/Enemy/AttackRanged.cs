using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class AttackRanged : MonoBehaviour
{


    [SerializeField] private float attackRadius;
    private Vector3 distanceToPlayer;

    private GameObject player;

    private NavMeshAgent agent;

    private Animator anim;
    public Vector3 animDirection;


    public bool isAttacking = false;
    private bool canAttack = true;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float projectileSpeed = 15f;

    private EnemyHealth healthScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent =  GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        healthScript = GetComponent<EnemyHealth>();
        healthScript.isInvunrable = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.updateRotation = false;

        CalculateDistanceToPlayer();
        EnemyAnimations();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && distanceToPlayer.magnitude > attackRadius)
        {
            agent.destination = distanceToPlayer * 1.5f ;
        }

        else if(other.gameObject.CompareTag("Player") && distanceToPlayer.magnitude < attackRadius && canAttack)
        {
            healthScript.isInvunrable = false;
            StartCoroutine(EnemyAttack());
        }

    else if (canAttack) 
        {
            StartCoroutine(EnemyAttack());
        }
    }

    void CalculateDistanceToPlayer()
    {
        distanceToPlayer = transform.position - player.transform.position;

    }

    void EnemyAnimations()
    {

        animDirection = (player.transform.position - transform.position).normalized;
        animDirection.y = 0;
        anim.SetFloat("animMoveMagnitude", animDirection.magnitude);
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            anim.SetTrigger("Idle");
            anim.SetFloat("horizontalMovement", animDirection.x);
            anim.SetFloat("verticalMovement", animDirection.z);
        }
        
        else if (agent.remainingDistance > agent.stoppingDistance)
        {
            anim.SetFloat("horizontalMovement", -animDirection.x);
            anim.SetFloat("verticalMovement", -animDirection.z);
        }

    }


    void RangedAttack()
    {
        if (projectilePrefab == null || firePoint == null) return;

      
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

    
        Vector3 direction = (player.transform.position - firePoint.position).normalized;


        projectile.transform.forward = direction;

        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        canAttack = false;


        //attackSound.pitch = Random.Range(0.5f, 0.7f);
        //attackSound.PlayOneShot(attackSound.clip);


        agent.enabled = false;

        anim.SetTrigger("attack");

        RangedAttack();
        yield return new WaitForSeconds(attackDuration);

        agent.enabled = true;
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }
}
