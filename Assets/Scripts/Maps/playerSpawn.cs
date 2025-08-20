using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public Transform spawn;
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Player.transform.position = spawn.transform.position;
      
    }


}
