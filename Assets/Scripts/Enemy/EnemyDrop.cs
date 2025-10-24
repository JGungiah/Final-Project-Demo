using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyDrop : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed;
    public GameObject VFX;
    public GameObject coinHead;
    private bool hasMoved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasMoved)
        {
            StartCoroutine(FollowDelay());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasMoved)
        {
            coinHead.SetActive(false);
            VFX.SetActive(true);
            Destroy(this.gameObject, 0.5f);
        }
    }

    private IEnumerator FollowDelay()
    {
        yield return new WaitForSeconds(2f);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        hasMoved = true;
    }
}
