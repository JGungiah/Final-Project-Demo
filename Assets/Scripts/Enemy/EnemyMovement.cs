using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float knockbackDuration;

    [Header("Animation Settings")]
    [SerializeField] private Animator animator; // Animator with 8-dir blend tree
    [SerializeField] private string animParamX = "horizontalMovement";
    [SerializeField] private string animParamY = "verticalMovement";

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
        agent.updateRotation = false;
        if (player != null && !isBeingKnockedBack)
        {
            Movement();
            Update8DirectionAnimation();
        }
    }

    void Movement()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.destination = player.transform.position;
        }
    }

    void Update8DirectionAnimation()
    {
        // Get movement direction in world space
        Vector3 dir = (player.transform.position - transform.position).normalized;

        // Remove vertical influence (top-down perspective)
        dir.y = 0;

        // Normalize direction for animation
        Vector3 localDir = transform.InverseTransformDirection(dir);

        // Send to animator (works with 8-dir blend tree)
        animator.SetFloat(animParamX, dir.x);
        animator.SetFloat(animParamY, dir.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider") && attackScript.isAttacking && !isBeingKnockedBack)
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

        yield return new WaitForSeconds(0.05f);
        agent.enabled = true;
        isBeingKnockedBack = false;
    }
}
