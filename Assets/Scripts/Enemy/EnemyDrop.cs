using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyDrop : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards( transform.position, player.transform.position,speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
