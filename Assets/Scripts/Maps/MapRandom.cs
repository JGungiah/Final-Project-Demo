using UnityEngine;


public class MapRandom : MonoBehaviour
{
    public float[] maps;
    float chosenmap;
    int generatemap;
    private void Update()
    {
        Debug.Log(maps[generatemap]);
    }
    public void mapRandomiser() 
    {
        generatemap = Random.Range(0, maps.Length);
        chosenmap = maps[generatemap];
       
    }
}
