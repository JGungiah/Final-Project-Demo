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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent =  GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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

        else if(other.gameObject.CompareTag("Player") && distanceToPlayer.magnitude < attackRadius)
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

    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        canAttack = false;


        //attackSound.pitch = Random.Range(0.5f, 0.7f);
        //attackSound.PlayOneShot(attackSound.clip);


        agent.enabled = false;

        anim.SetTrigger("attack");

        yield return new WaitForSeconds(attackDuration);

        agent.enabled = true;
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }
}
