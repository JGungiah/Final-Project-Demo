using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    public GameObject playerPrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindAnyObjectByType<Player>() == null)
        {
            Instantiate(playerPrefab, new Vector3 (0,1,0), Quaternion.Euler(0f, 45f, 0f));
        }
    }
}
