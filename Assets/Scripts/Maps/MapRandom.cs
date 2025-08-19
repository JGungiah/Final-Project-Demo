using UnityEngine;


public class MapRandom : MonoBehaviour
{
    public float[] maps = [0,1,2,3,4,5,6,7,8];
    public void mapRandomiser() 
    {
        int generatemap = Random.Range(0, maps.Length);
        float chosenmap = maps[generatemap];
    }
}
