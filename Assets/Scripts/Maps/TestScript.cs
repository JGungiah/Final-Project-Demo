using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public List<string> sceneNames;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneGenerator();
        }
    }
    public void SceneGenerator()
    {
        int RandIndex = Random.Range(0, sceneNames.Count);
        string scenetoload = sceneNames[RandIndex];
        SceneManager.LoadScene(scenetoload);
    }
}
