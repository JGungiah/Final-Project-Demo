using System.Collections;
using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    private GameObject player;
    private Health health;
    [SerializeField] private float poisonLifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<Health>();
        StartCoroutine(poisonDestroy());  
    }

   IEnumerator poisonDestroy() 
    {
        yield return new WaitForSeconds(poisonLifetime);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {

        }
    }
}
