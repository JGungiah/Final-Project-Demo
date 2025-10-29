using System.Collections;
using UnityEngine;

public class PlayerTooFar : MonoBehaviour
{
    public JormungandrAttack FireProj;
    public GameObject Jorm;
    public Jormungandr JormHealth;
   public float timer = 15f;
    public bool hasAttacked;
    [SerializeField] private float distance;

    private GameObject player;
    private bool hasattacked2;
    private Vector3 distanceToPlayer;

    private void Start()
    {
        FireProj = Jorm.GetComponent<JormungandrAttack>();
        JormHealth = Jorm.GetComponent<Jormungandr>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        DistanceToPlayer();

        if (distanceToPlayer.magnitude < distance) // Enter
        {
            hasattacked2 = true;
            hasAttacked = true;

        }
         if (distanceToPlayer.magnitude > distance && hasattacked2) //Exit
        {
            hasAttacked = false;
            StartCoroutine(waitToattack());
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        hasAttacked = true;
         

    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player")) 
    //    {
    //        hasAttacked = false;
    //        StartCoroutine(waitToattack());

    //    }
    //}

    void DistanceToPlayer()
    {
        distanceToPlayer = transform.position - player.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
    public IEnumerator waitToattack() 
    {
        if (!hasAttacked)
        {
           
            yield return new WaitForSeconds(timer);
            if (hasAttacked) 
            {
                yield break;
            }
            else 
            {
                FireProj.RangedAttack();
                
            }
            hasAttacked = false;
            StartCoroutine(waitToattack());
            hasattacked2 = true;
        }
    }
    
}
