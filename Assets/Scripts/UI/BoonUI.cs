using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    private Vector3 originalSize;
    public Image borderImage;
    private AudioSource hoverSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         hoverSound = GetComponent<AudioSource>();
         originalSize = button.transform.localScale ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverSound.PlayOneShot(hoverSound.clip);
        button.transform.localScale = originalSize * 1.2f;
        borderImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.localScale = originalSize;
        borderImage.gameObject.SetActive(false);
    }
}
