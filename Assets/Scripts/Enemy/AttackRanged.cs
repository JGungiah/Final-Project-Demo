using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

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

    private float originalAttackCooldown;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform rotationPivot;
    [SerializeField] private float projectileSpeed = 15f;

    private EnemyHealth healthScript;

    private bool playerInsideTrigger = false;

    public bool attack;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        healthScript = GetComponent<EnemyHealth>();
        healthScript.isInvunrable = true;
        originalAttackCooldown = attackCooldown;
    }

    void Update()
    {
        agent.updateRotation = false;

        CalculateDistanceToPlayer();
        HandleEnemyLogic();
        EnemyAnimations();

        rotationPivot.rotation = Quaternion.LookRotation(animDirection, Vector3.up);

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            print(2);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
        }
           
    }

    void CalculateDistanceToPlayer()
    {
        distanceToPlayer = transform.position - player.transform.position;
    }
    void HandleEnemyLogic()
    {
        float distance = distanceToPlayer.magnitude;

        if (!playerInsideTrigger)
        {
            if (canAttack)
            {
                StartCoroutine(EnemyAttack());
                attackCooldown = originalAttackCooldown;
                healthScript.isInvunrable = true;
            }
               
        }

        if (distance < attackRadius)
        {
            agent.isStopped = false;
            agent.destination = transform.position + distanceToPlayer.normalized * 1.5f;
            healthScript.isInvunrable = true;
        }

        if (playerInsideTrigger)
        {
            healthScript.isInvunrable = false;
            agent.isStopped = true;
            if (canAttack)
            {
                attackCooldown = 2;
                StartCoroutine(EnemyAttack());
            }
        }

    }

 

  
    void EnemyAnimations()
    {
        animDirection = (player.transform.position - transform.position).normalized;
        animDirection.y = 0;

        anim.SetFloat("animMoveMagnitude", agent.velocity.magnitude);

        bool isMoving = agent.velocity.magnitude > 0.1f;

        if (!isMoving && !isAttacking)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.ResetTrigger("attack");
                anim.SetTrigger("Idle");
            }
        }

        if (playerInsideTrigger || distanceToPlayer.magnitude > attackRadius)
        {
            anim.SetFloat("horizontalMovement", animDirection.x);
            anim.SetFloat("verticalMovement", animDirection.z);
        }
    }


    public void RangedAttack()
    {
        
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Vector3 direction = (player.transform.position - firePoint.position).normalized;
        projectile.transform.forward = direction;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * projectileSpeed;
        
    }

    private IEnumerator EnemyAttack()
    {
        anim.ResetTrigger("Idle");

        isAttacking = true;
        canAttack = false;

        agent.destination = transform.position; 
        agent.isStopped = true;

        anim.SetTrigger("attack");

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        agent.isStopped = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    }
