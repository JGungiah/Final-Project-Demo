using System.Collections;
using UnityEngine;

public class TailCollider : MonoBehaviour
{
    private GameObject player;
    private Health HealthScript;
    public bool isIncollider;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        HealthScript = player.GetComponent<Health>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isIncollider = true;
            StartCoroutine(SwipeAttack());
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
            Debug.Log("start");
            yield return new WaitForSeconds(3f);
            if (!isIncollider) yield break;
            HealthScript.currentHealth -= 40f;
            Debug.Log("end");
        }
    }
}
