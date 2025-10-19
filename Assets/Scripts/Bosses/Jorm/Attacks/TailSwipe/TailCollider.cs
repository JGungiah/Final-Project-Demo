using System.Collections;
using UnityEngine;

public class TailCollider : MonoBehaviour
{
    public GameObject player;
    private Health HealthScript;
    public bool isIncollider;
    private void Start()
    {
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
    public IEnumerator SwipeAttack()
    {
        while (isIncollider)
        {
            Debug.Log("start");
            yield return new WaitForSeconds(3f);
            HealthScript.TakeDamage(40f);
            Debug.Log("end");
        }
    }
}
