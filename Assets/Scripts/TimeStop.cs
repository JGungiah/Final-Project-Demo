using UnityEngine;
using System.Collections;
public class TimeStop : MonoBehaviour
{
    public GameObject mainCam;
    public bool changeCam;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeCam)
        {
            mainCam.SetActive(false);
        }
    }

 

    public IEnumerator DissolveEffect()
    {

        float dissolveTime = 3f;
        float elapsedTime = 0f;
        float startValue = 1f;
        float endValue = 0f;
        Animator anim = GetComponent<Animator>();
        string dissolveProperty = "_Dissolve_Amount";

        while (elapsedTime < dissolveTime)
        {
            anim.enabled = false;
            float dissolveValue = Mathf.Lerp(endValue, startValue, elapsedTime / dissolveTime);
            spriteRenderer.material.SetFloat(dissolveProperty, dissolveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 0;

    }
}
