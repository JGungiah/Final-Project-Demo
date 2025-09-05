using UnityEngine;
using UnityEngine.AI;
public class AttackRanged : MonoBehaviour
{


    [SerializeField] private float attackRadius;
    private Vector3 distanceToPlayer;

    private GameObject player;

    private NavMeshAgent agent;

    private Animator anim;
    public Vector3 animDirection;

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
}
