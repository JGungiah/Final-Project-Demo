using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerInteract : MonoBehaviour
{
    public List<string> sceneNames;
    public GameObject interactImage;
    private bool hasBeenPressed;
    private Scene currentScene;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI roomNumberText;
    public TextMeshProUGUI roomNumber;
    private float numberOfRoomsCompleted = 0;
    void Awake()
    {

    }

  
    void Update()
    {
        waveText.text = " Wave "  +  enemySpawner.numberOfWavesCompleted;
        roomNumber.text = numberOfRoomsCompleted.ToString();

        if (hasBeenPressed)
        {
            interactImage.gameObject.SetActive(false);
        }

        CheckScene();
        
        if (currentScene.name == "LobbyRoom")
        {
            waveText.gameObject.SetActive(false);
            roomNumberText.gameObject.SetActive(false);
            roomNumber.gameObject.SetActive(false);

            enemySpawner.numberOfWavesCompleted = 0;
            numberOfRoomsCompleted = 0;
        }
        else
        {
            waveText.gameObject.SetActive(true);
            roomNumberText.gameObject.SetActive(true);
            roomNumber.gameObject.SetActive(true);

        }

        
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene("LobbyRoom");
    }
    private void CheckScene()
    {
         currentScene = SceneManager.GetActiveScene();

    }
    public void SceneGenerator() 
    {
        int RandIndex = Random.Range(0, sceneNames.Count);
        
        string scenetoload = sceneNames[RandIndex];
        SceneManager.LoadScene(scenetoload);
        numberOfRoomsCompleted ++;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "InteractDoor")
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                interactImage.gameObject.SetActive(true);
                SceneGenerator();
                hasBeenPressed = true;
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
