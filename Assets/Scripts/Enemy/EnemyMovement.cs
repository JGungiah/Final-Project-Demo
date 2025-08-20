using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
   
    
    private float horizontalMovement;
    private float verticalMovement;
    private Animator anim;
    public Vector3 animDirection;
    private Vector3 velocity;

  
    private AttackMelee attackMeleeScript;
    private GameObject player;
    private NavMeshAgent agent;
   
    private Health healthScript;
  
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attackMeleeScript = GetComponent<AttackMelee>();
    }

    void Update()
    {
        agent.updateRotation = false;

        if (player != null && !attackMeleeScript.isBeingKnockedBack)
        {
            if (!attackMeleeScript.isAttacking) 
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

        if (attackMeleeScript.isAttacking)
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

   
}
