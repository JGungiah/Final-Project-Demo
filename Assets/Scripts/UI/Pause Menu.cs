using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject audioMenu;
    private GameObject player;
    private playerInteract interact;
    private Scene currentScene;

    public bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        interact = player.GetComponent<playerInteract>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !controlsMenu.activeSelf && !audioMenu.activeSelf)
        {
            interact.canvasActive = false;
            isPaused = true;
            
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            

        }
        //else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf || Input.GetKey(KeyCode.Escape) && controlsMenu.activeSelf || Input.GetKey(KeyCode.Escape) && audioMenu.activeSelf)
        //{
          
        //}
    }

    public void resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        audioMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 1;
        //if (currentScene.name != "LobbyRoom")
        //{
        //    interact.canvasActive = true;
        //}

    }

    public void ControlsMenu()
    {
        controlsMenu.SetActive(true);
        audioMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void AudioMenu()
    {
        audioMenu.SetActive(true);
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        audioMenu.SetActive(false);
    }
}
