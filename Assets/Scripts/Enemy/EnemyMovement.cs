using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    [SerializeField] private float range;

    [SerializeField] private float patrolCoolDown;
    private bool canCheck = true;

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

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && attackMeleeScript.isAttacking)
        {
            //agent.isStopped = true;
            StartCoroutine(Idle());
        }
        else
        {

            agent.isStopped = false;
            agent.speed = attackMeleeScript.originalSpeed;

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
                else if (agent.isStopped)
                {
                    if (!footStepsSound.isPlaying)
                    {
                        footStepsSound.Stop();
                    }

                    stepTimer = 0f;
                }
            }
        }

        if (!agent.isStopped && agent.hasPath)
        {
            velocity = agent.desiredVelocity.normalized * agent.speed;
            agent.Move(velocity * Time.deltaTime);
        }

        if (attackMeleeScript.hasAttacked)
        {
            StartCoroutine(Idle());
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

        if (distanceToWalkPoint.magnitude < 1 && canCheck)
        {
            canCheck = false;
            StartCoroutine(PatrolCoolDown());
        }


    }

    IEnumerator PatrolCoolDown()
    {
        patrolCoolDown = Random.Range(1, 3);
        yield return new WaitForSeconds(patrolCoolDown);
        walkPointSet = false;
        canCheck = true;
    }
    void ClosestWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        GameObject closest = waypoints[0];
        float closestDistance = Vector3.Distance(transform.position, closest.transform.position);

        foreach (GameObject waypoint in waypoints)
        {
            float distance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = waypoint;
            }
        }

        walkPoint = new Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z);
        walkPointSet = true;
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {


            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    void RandomWaypoint()
    {
        Vector3 point;
        if (RandomPoint(transform.position, range, out point))
        {
            range = Random.Range(30, 40);

            walkPoint = new Vector3(point.x, transform.position.y, point.z);
            walkPointSet = true;

        }


    }

    void DistanceToPlayer()
    {
        canChase = Physics.CheckSphere(transform.position, chaseRadius, playerMask);
    }

    void Chase()
    {
        if (!hasWaypoint)
        {
            ClosestWaypoint();
        }


        if (agent.isActiveAndEnabled)
        {

            agent.destination = walkPoint;
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

        if (attackMeleeScript.isAttacking)
        {
            anim.SetFloat("animMoveMagnitude", animDirection.magnitude);
        }




    }

    IEnumerator Idle()
    {
        anim.SetFloat("animMoveMagnitude", 0);
        yield return new WaitForSeconds(1);
        anim.SetFloat("animMoveMagnitude", animDirection.magnitude);
        agent.isStopped = false;
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

