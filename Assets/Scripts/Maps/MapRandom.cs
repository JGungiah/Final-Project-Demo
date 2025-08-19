using UnityEngine;


public class MapRandom : MonoBehaviour
{
    public float[] maps;
    public void mapRandomiser() 
    {
        int generatemap = Random.Range(0, maps.Length);
        float chosenmap = maps[generatemap];
    }
}
