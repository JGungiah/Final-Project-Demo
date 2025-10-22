using System.Collections;
using UnityEngine;

public class TailCollider : MonoBehaviour
{
    private GameObject player;
    private Health HealthScript;
    public bool isIncollider;
    public Jormungandr Jorm;
    public GameObject JormBody;

    private bool isMidhealth;
    private bool isLowhealth;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        HealthScript = player.GetComponent<Health>();
        JormBody = GameObject.FindWithTag("Jormungandr");
        Jorm = JormBody.GetComponent<Jormungandr>();
    }

    private void Update()
    {
        Swipe();
    }
    public void Swipe() 
    {
        if(Jorm.currentHealth <= 700 && isIncollider && !isMidhealth)
        {
            StartCoroutine(SwipeAttack());
            isMidhealth = true;
        }
        if (Jorm.currentHealth <= 400 && isIncollider && !isLowhealth)
        {
            StartCoroutine(SwipeAttack());
            isLowhealth = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isIncollider = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIncollider = false;
        }
        }
    IEnumerator SwipeAttack()
    {
        while (isIncollider)
        {
          
            yield return new WaitForSeconds(3f);
            if (!isIncollider) yield break;
            HealthScript.currentHealth -= 40f;
            
        }
    }
}
