using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OutroPanel : MonoBehaviour
{
    public Animator[] Animators;
    public AudioSource[] AudioSources;
    public TextMeshProUGUI panelText;
    public Animator TextAnim;

    public GameObject Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Panel1());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Panel1()
    {
        yield return new WaitForSeconds(1);
        AudioSources[0].Play();
        Animators[0].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = "The deed was done. Yggdrasil—lifeline of gods and worlds—fell beneath my axe";

        yield return new WaitForSeconds(11);

        AudioSources[1].Play();
        Animators[1].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = "Then the wind shifted… and";

        yield return new WaitForSeconds(9.5f);

        AudioSources[2].Play();
        Animators[2].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = "the sky began to rain bodies";

        yield return new WaitForSeconds(9.5f);

        AudioSources[3].Play();
        Animators[3].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = " Odin. Thor. All of them—lifeless. Their power, their glory… gone with the tree they fed upon. This is the price of breaking destiny.";
        Button.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
