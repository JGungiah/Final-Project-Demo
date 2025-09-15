using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{
       
    private float horizontalMovement;
    private float verticalMovement;
    private Animator anim;
    public Vector3 animDirection;
    private Vector3 velocity;

    private GameObject[] waypoints;
    private int randomWaypoint;
    private bool hasWaypoint = false;

    private AttackMelee attackMeleeScript;
    private GameObject player;
    private NavMeshAgent agent;
   
    private Health healthScript;
   
    public AudioSource footStepsSound;

    [SerializeField] private float stepInterval = 0.5f;
    private float stepTimer;

    [SerializeField] private float chaseRadius;
    [SerializeField] public bool canChase;
    [SerializeField] private LayerMask playerMask;

    private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;
    private bool walkPointSet = false;
    private bool hasBeenDetected = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attackMeleeScript = GetComponent<AttackMelee>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void Update()
    {
        if (!canChase)
        {
            agent.destination = transform.position;
        }
       

        DistanceToPlayer();
      
        agent.updateRotation = false;

        if (player != null && !attackMeleeScript.isBeingKnockedBack)
        {
            if (!attackMeleeScript.isAttacking) 
            {
                if (canChase || hasBeenDetected)
                {
                    hasBeenDetected = true;
                    EnemyAnimations();
                    Chase();
                }
                else
                {
                    PatrolAnimations();
                    Patrol();
                }

                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    footStepsSound.pitch = Random.Range(1.0f, 1.4f);
                    footStepsSound.PlayOneShot(footStepsSound.clip);
                    stepTimer = stepInterval;
                }
                else if (agent.enabled == false)
                {
                    if (!footStepsSound.isPlaying)
                    {
                        footStepsSound.Stop();
                    }
           
                    stepTimer = 0f;
                }
            }
        }

        if (agent.hasPath)
        {
            velocity = agent.desiredVelocity.normalized * agent.speed;
            agent.Move(velocity * Time.deltaTime);
        }


    }


    void Patrol()
    {
        if (!walkPointSet)
        {
            RandomWaypoint();
        }

        if (walkPointSet)
        {
            agent.destination = walkPoint;
            

        }

       Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;   
        }

       
    }
   

    void RandomWaypoint()
    {
        float RandomX = Random.Range(-walkPointRange, walkPointRange);
        float RandomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + RandomX , transform.position.y , transform.position.z + RandomZ);

        walkPointSet = true;
    }

    void DistanceToPlayer()
    {
        canChase = Physics.CheckSphere(transform.position, chaseRadius, playerMask);
    }
    
    void Chase()
    {
        if (!hasWaypoint)
        {
            foreach (GameObject waypoint in waypoints)
            {
                randomWaypoint = Random.Range(0, waypoints.Length);
                hasWaypoint = true;
            }
        }
       
       
        if (agent.isActiveAndEnabled) 
        { 
         
           agent.destination = waypoints[randomWaypoint].transform.position; 
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

    void PatrolAnimations()
    {
       
        Vector3 moveDir = agent.velocity.normalized;
        moveDir.y = 0;

        horizontalMovement = moveDir.x;
        verticalMovement = moveDir.z;

        anim.SetFloat("horizontalMovement", horizontalMovement);
        anim.SetFloat("verticalMovement", verticalMovement);
        anim.SetFloat("animMoveMagnitude", moveDir.magnitude);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
