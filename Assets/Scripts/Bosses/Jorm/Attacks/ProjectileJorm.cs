using UnityEngine;

public class ProjectileJorm : MonoBehaviour
{
    public float speed = 10f;
    public GameObject poisonPoolPrefab;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            SpawnPool();
            Destroy(gameObject);
        }
    }

    void SpawnPool()
    {
        Instantiate(poisonPoolPrefab, transform.position, Quaternion.identity);
    }
}
