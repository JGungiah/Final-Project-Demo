using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject loadingScreenUI;
    public Animator Loadanim;
    public Transform spawnPoint;
    public float maxCheckTime = 3f;

    private GameObject player;
    private CharacterController controller;

    public bool isLoading = false;
    void OnEnable()
    {
        controller = FindAnyObjectByType<CharacterController>();
        
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

    private void Update()
    {
        if (Loadanim.GetBool("FadeIn"))
        {
            isLoading = true;
            StartCoroutine(ResetBool());

        }
  
  
    }


    IEnumerator HandleSceneLoad()
    {
        Time.timeScale = 0f;
       
        player = GameObject.FindWithTag("Player");

        yield return null;

        GameObject spawnObj = GameObject.FindWithTag("SpawnPoint");
        Vector3 targetPos = spawnObj != null ? spawnObj.transform.position : Vector3.zero;
        yield return null;

        controller.enabled = false; 

        player.transform.position = targetPos;

        controller.enabled = true;


        yield return new WaitForSecondsRealtime(2f);

       
        yield return null;
        Time.timeScale = 1f;
        
    }

    IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(ReturnUI());
    }

    IEnumerator ReturnUI()
    {
        yield return new WaitForSeconds(0);
        isLoading = false;
    }
}