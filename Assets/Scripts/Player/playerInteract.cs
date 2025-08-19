using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerInteract : MonoBehaviour
{
    public List<string> sceneNames;
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
    public void SceneGenerator() 
    {
        int RandIndex = Random.Range(0, sceneNames.Count);
        string scenetoload = sceneNames[RandIndex];
        SceneManager.LoadScene(scenetoload);
    }
}
