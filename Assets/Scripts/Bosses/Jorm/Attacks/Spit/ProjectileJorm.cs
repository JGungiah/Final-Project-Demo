using UnityEngine;

public class ProjectileJorm : MonoBehaviour
{
    public float speed = 10f;
    public GameObject poisonPoolPrefab;
    private AudioSource acidImpact;

    private void Start()
    {
        acidImpact = GetComponent<AudioSource>();   
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
        acidImpact.Play();
        Instantiate(poisonPoolPrefab, transform.position, Quaternion.identity);
    }
}
