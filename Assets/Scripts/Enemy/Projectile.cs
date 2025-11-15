using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime;
    public GameObject VFX;
    private Rigidbody body;
    private AudioSource impactSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        impactSound = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           impactSound.PlayOneShot(impactSound.clip);
           body.linearVelocity = Vector3.zero;
           VFX.SetActive(true);
           Destroy(this.gameObject, 1);
        }
    }
}
