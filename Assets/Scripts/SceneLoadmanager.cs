using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject loadingScreenUI;
    public Transform spawnPoint;
    public float maxCheckTime = 3f;

    private GameObject player;
    private CharacterController controller;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(HandleSceneLoad());
    }

  

    IEnumerator HandleSceneLoad()
    {
        Time.timeScale = 0f;
        loadingScreenUI.SetActive(true);

        player = GameObject.FindWithTag("Player");

        yield return null;

        GameObject spawnObj = GameObject.FindWithTag("SpawnPoint");
        Vector3 targetPos = spawnObj != null ? spawnObj.transform.position : Vector3.zero;
        yield return null;
      

        //if (Physics.Raycast(targetPos + Vector3.up * 2, Vector3.down, out RaycastHit hit, 20f))
     //   {
           player.transform.position = targetPos;
        Debug.Log(targetPos);
        Debug.Log(player.transform.position);
      //  }
     //   else
     //   {
      //      
     //       player.transform.position = hit.point;
    //    }

        yield return new WaitForSecondsRealtime(4f);

        loadingScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }
}