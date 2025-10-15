using UnityEngine;

public class ProjectileJorm : MonoBehaviour
{
    public float speed = 10f;
    public GameObject poisonPoolPrefab;
    public LayerMask groundLayer; 

    private Vector3 direction;

    public void Init(Vector3 targetPosition)
    {
       
        direction = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        
        transform.position += direction * speed * Time.deltaTime;
    }

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
