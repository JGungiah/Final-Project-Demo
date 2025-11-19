using UnityEngine;

public class loadNewScene : MonoBehaviour
{
    private GameObject player;
    private playerInteract interactScript;

    public bool nextScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        interactScript = player.GetComponent<playerInteract>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactScript.canTeleport = true;
            if (Input.GetKeyUp(KeyCode.E))
            {
                
                if (interactScript.NormRooms)
                {
                    StartCoroutine(interactScript.SceneChangeDelay());
                }
                else if (interactScript.YggdrasilRooms)
                {
                    {
                        StartCoroutine(interactScript.SceneChangeDelayYggdrasil());
                    }

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactScript.canTeleport = false;
        }
    }
}