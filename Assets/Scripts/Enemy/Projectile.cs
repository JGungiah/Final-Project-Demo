using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
