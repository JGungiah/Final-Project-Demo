using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    private Vector3 originalSize;
    public Image borderImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         originalSize = button.transform.localScale ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        button.transform.localScale = originalSize * 1.2f;
        borderImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.localScale = originalSize;
        borderImage.gameObject.SetActive(false);
    }
}
