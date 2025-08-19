using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float knockbackDuration;

    private float horizontalMovement;
    private float verticalMovement;
    private Animator anim;
    private Vector3 animDirection;

    private GameObject player;
    private NavMeshAgent agent;
    private Attack attackScript;
    private bool isBeingKnockedBack = false;

    private bool isAttacking = false;

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
            Movement();
            EnemyAnimations();
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
        if (other.gameObject.CompareTag("PlayerCollider") && attackScript.isAttacking && !isBeingKnockedBack)
        {
            StartCoroutine(KnockBack());
            
        }
        else if (other.gameObject.CompareTag("PlayerCollider") && !isAttacking)
        {
            isAttacking = true;
            anim.SetFloat("AttackHorizontal", animDirection.x);
            anim.SetFloat("AttackVertical", animDirection.z);
            anim.SetTrigger("attack");
        }
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
            isAttacking = false;
            
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
