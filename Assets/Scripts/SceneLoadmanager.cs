using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenUI; 
    [SerializeField] private GameObject player;         

    public void LoadScene(string sceneName)
    {
        StartCoroutine(HandleSceneLoad(sceneName));
    }

    private IEnumerator HandleSceneLoad(string sceneName)
    {
       
        Time.timeScale = 0f;
        loadingScreenUI.SetActive(true);

     
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone)
            yield return null;

       
        yield return null;

      
        GameObject spawnObj = GameObject.FindWithTag("SpawnPoint");
        Vector3 targetPos = spawnObj != null ? spawnObj.transform.position : Vector3.zero;

       
        if (Physics.Raycast(targetPos + Vector3.up * 2f, Vector3.down, out RaycastHit hit, 20f))
        {
            player.transform.position = hit.point;
        }
        else
        {
            player.transform.position = targetPos;
        }

        
        yield return new WaitForSecondsRealtime(2f);

       
        loadingScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }
}