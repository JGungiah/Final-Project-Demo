using System.Collections;
using UnityEngine;

public class PlayerTooFar : MonoBehaviour
{
    public JormungandrAttack FireProj;
    public GameObject Jorm;
    public Jormungandr JormHealth;
   public float timer = 15f;
    public bool hasAttacked;
    private void Start()
    {
        FireProj = Jorm.GetComponent<JormungandrAttack>();
        JormHealth = Jorm.GetComponent<Jormungandr>();
    }

    private void Update()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasAttacked = true;
         

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            hasAttacked = false;
            StartCoroutine(waitToattack());
            
        }
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

        }
    }
    
}
