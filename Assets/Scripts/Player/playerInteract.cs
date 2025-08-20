using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerInteract : MonoBehaviour
{
    public List<string> sceneNames;
    public GameObject interactImage;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
    public void SceneGenerator() 
    {
        int RandIndex = Random.Range(0, sceneNames.Count);
        string scenetoload = sceneNames[RandIndex];
        SceneManager.LoadScene(scenetoload);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "InteractDoor")
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                interactImage.gameObject.SetActive(true);
                SceneManager.LoadScene("LobbyRoom");
            }
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractDoor")
        {
            interactImage.gameObject.SetActive(false);
        }
    }
}
