using UnityEngine;

public class BoonItem : MonoBehaviour
{
    [SerializeField] private Transform panel;
    private Transform boonpanel;
    private Transform boonCanvas;
    private GameObject gameManager;
    private GameObject player;
    private Transform canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");

        if (gameManager != null)
        {
            boonCanvas = gameManager.transform.Find("Boon UI");
            boonpanel = boonCanvas.transform.Find("Panel");
        }
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
                panel.gameObject.SetActive(true);
                canvas.gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panel.gameObject.SetActive(false);
            canvas.gameObject.SetActive(true);
            boonpanel.gameObject.SetActive(false);
        }
    }

    public void BoonPanel()
    {
        boonpanel.gameObject.SetActive(true );
        panel.gameObject.SetActive(false);
    }
}
