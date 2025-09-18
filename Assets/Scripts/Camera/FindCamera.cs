using UnityEngine;

public class FindCamera : MonoBehaviour
{
    private Canvas canvas;
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
        canvas.worldCamera = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
