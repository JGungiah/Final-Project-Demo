using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CameraFind : MonoBehaviour
{
    private Camera mainCam;
    private GameObject camera;
    private VideoPlayer videoPlayer; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        mainCam = camera.GetComponent<Camera>();
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.targetCamera = mainCam;
        //StartCoroutine(video());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator video()
    {
        yield return new WaitForSeconds(2);
        videoPlayer.Play();
    }
}
