using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;
    
    private float horizontalMovement;
    private float verticalMovement;
    private Animator anim;
    private Vector3 animDirection;
    private Vector3 velocity;



    private GameObject player;
    private NavMeshAgent agent;
    private Attack attackScript;
    private bool isBeingKnockedBack = false;

    public bool isAttacking = false;
    private bool canAttack = true;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attackScript = player.GetComponent<Attack>();
      
    }

    void Update()
    {
        agent.updateRotation = false;

        if (player != null && !isBeingKnockedBack)
        {
            if (!isAttacking) 
            {
                Movement();
                EnemyAnimations();
            }
        }

        if (agent.hasPath)
        {
            velocity = agent.desiredVelocity.normalized * agent.speed;
            agent.Move(velocity * Time.deltaTime);
        }

        if (isAttacking)
        {
            agent.enabled = false;
        }
    }

    void Movement()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.destination = player.transform.position;
        }
      
    }

    void EnemyAnimations()
    {
        
        animDirection = (player.transform.position - transform.position).normalized;
        animDirection.y = 0;

        horizontalMovement = animDirection.x;
        verticalMovement = animDirection.z;

        anim.SetFloat("horizontalMovement", horizontalMovement);
        anim.SetFloat("verticalMovement", verticalMovement);
        anim.SetFloat("animMoveMagnitude", animDirection.magnitude);

  
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && attackScript.isAttacking && !isBeingKnockedBack)
        {
            StartCoroutine(KnockBack());
            
        }
        else if (other.gameObject.CompareTag("PlayerCollider") && !isAttacking && canAttack)
        {
            StartCoroutine(EnemyAttack());
        }
    }

    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        canAttack = false;

       
        agent.enabled = false;

       
        anim.SetFloat("AttackHorizontal", animDirection.x);
        anim.SetFloat("AttackVertical", animDirection.z);
        anim.SetTrigger("attack");

       
       
        yield return new WaitForSeconds(attackDuration);

       
        agent.enabled = true;
        isAttacking = false;


        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            agent.enabled = false;
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            agent.enabled = true;
        }
    }

    IEnumerator KnockBack()
    {
        isBeingKnockedBack = true;
        agent.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            transform.position += attackScript.knockbackDirection * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);
        agent.enabled = true;
        isBeingKnockedBack = false;
    }
}
