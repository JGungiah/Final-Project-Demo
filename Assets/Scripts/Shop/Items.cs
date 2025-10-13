using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private GameObject player;
    private Transform canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            canvas = player.transform.Find("Canvas");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                panel.SetActive(true);
                canvas.gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                panel.SetActive(false);
            canvas.gameObject.SetActive(true);
        }
    }
}
