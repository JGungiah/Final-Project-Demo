using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float knockbackDuration;

    private GameObject player;
    private NavMeshAgent agent;
    private Attack attackScript;
    private bool isBeingKnockedBack = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        attackScript = player.GetComponent<Attack>(); 
    }

    void Update()
    {
       
        if (player != null && !isBeingKnockedBack)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.destination = player.transform.position;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player") && attackScript.isAttacking && !isBeingKnockedBack)
        {
            StartCoroutine(KnockBack());
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

        agent.enabled = true; 
        isBeingKnockedBack = false;
    }


}
