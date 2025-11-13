using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TimeStop : MonoBehaviour
{
    public GameObject mainCam;
    public bool changeCam;
    public SpriteRenderer spriteRenderer;


    public GameObject canvas;
    private Image image;
    private Color opacity;
    public Image button;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
        opacity = image.color;
        opacity.a = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (changeCam)
        {
            mainCam.SetActive(false);
            canvas.SetActive(false);
        }
        else
        {
            mainCam.SetActive(true);
        }

        //if (currentScene.name == "LobbyRoom")
        //{
            
        //}
    }

    void OnEnable()
    {
  
        SceneManager.sceneLoaded += OnSceneLoaded;

    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        anim.SetBool("HasDied", false);
        changeCam = false;
        image.color = opacity;
        //button.color = opacity;

        spriteRenderer.material.SetFloat("_Dissolve_Amount", 0);
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public IEnumerator DissolveEffect()
    {
       
        float dissolveTime = 3f;
        float elapsedTime = 0f;
        float startValue = 1f;
        float endValue = 0f;
       
        string dissolveProperty = "_Dissolve_Amount";

        while (elapsedTime < dissolveTime)
        {

            float dissolveValue = Mathf.Lerp(endValue, startValue, elapsedTime / dissolveTime);
            spriteRenderer.material.SetFloat(dissolveProperty, dissolveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 0;

    }
}
