using UnityEngine;
using UnityEngine.SceneManagement;

public class playerSpawn : MonoBehaviour
{
    public Transform spawn;
    GameObject player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spawn.position;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && spawn != null)
        {
            player.transform.position = spawn.position;
        }
    }


}
