using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
