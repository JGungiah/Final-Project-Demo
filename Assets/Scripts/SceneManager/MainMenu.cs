using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Color hoverColor;
    private Color originalColor;
    public AudioSource MenuSound;
    public Button Button;
    private Image buttonImage;
    public GameObject pausePanel;

    void Start()
    {
        buttonImage = Button.GetComponent<Image>();
        originalColor = buttonImage.color;

        MenuSound.time = 40f;
        MenuSound.Play();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Hover()
    {
        buttonImage.color = hoverColor;
    }

    public void OffHover()
    {
        buttonImage.color = originalColor;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
    }
}
