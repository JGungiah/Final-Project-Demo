using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject audioMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !controlsMenu.activeSelf && !audioMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;

        }
        //else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf || Input.GetKey(KeyCode.Escape) && controlsMenu.activeSelf || Input.GetKey(KeyCode.Escape) && audioMenu.activeSelf)
        //{
          
        //}
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        audioMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 1;
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
