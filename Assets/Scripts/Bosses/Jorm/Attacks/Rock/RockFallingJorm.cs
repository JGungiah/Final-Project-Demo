
using UnityEngine;

public class RockFallingJorm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) 
        {
            Destroy(this.gameObject);
        }
    }
}
