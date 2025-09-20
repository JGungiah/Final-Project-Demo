using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private static GameManager Instance;

    private void Awake()
    {
        if (FindAnyObjectByType<Player>() == null)
        {
            Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.Euler(0f, 45f, 0f));

            Invoke("RevertSelectedPrefabInstance", 10f);
        }

        if (Instance == null)
        {
     
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {

            Destroy(gameObject);
        }
    }

    

}
