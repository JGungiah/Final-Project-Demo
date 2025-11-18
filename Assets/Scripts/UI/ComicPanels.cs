using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
public class ComicPanels : MonoBehaviour
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
        panelText.text = "Before me stood Yggdrasil… the heart of all realms. But the moment I drew breath, the skies trembled… ";

        yield return new WaitForSeconds(11);

        AudioSources[1].Play();
        Animators[1].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = "and from the mist rose Jormungandr.";

        yield return new WaitForSeconds(9.5f);

        AudioSources[2].Play();
        Animators[2].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = "No more running. No more hesitation";

        yield return new WaitForSeconds(9.5f);

        AudioSources[3].Play();
        Animators[3].SetBool("Activate", true);
        TextAnim.SetTrigger("activate");
        panelText.text = "Either I carve my fate today… or I fall beneath the serpent’s shadow";
        Button.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
 
}
