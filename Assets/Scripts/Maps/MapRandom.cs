using UnityEngine;


public class MapRandom : MonoBehaviour
{
    public GameObject[] spawn;
    public void mapRandomiser() 
    {
        int generatemap = Random.Range(0, spawn.Length);
        GameObject chosenmap = spawn[generatemap];
    }
}
