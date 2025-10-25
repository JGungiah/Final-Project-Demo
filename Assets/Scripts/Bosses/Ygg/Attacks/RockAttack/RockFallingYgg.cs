
using UnityEngine;

public class RockFallingYgg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) 
        {
            Destroy(this.gameObject);
        }
    }
}
