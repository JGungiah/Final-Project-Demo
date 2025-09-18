using UnityEngine;
using System.Collections.Generic;

public class RandomizeBoons : MonoBehaviour
{
    public UpgradeScriptableObjects[] StatusBoons;
    public UpgradeScriptableObjects[] chosenBoons = new UpgradeScriptableObjects[3];

    private List<UpgradeScriptableObjects> availableBoons;
   
    private int randomBoon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeStatBoons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void RandomizeStatBoons()
    {
        availableBoons = new List<UpgradeScriptableObjects> (StatusBoons);

       for (int i = 0; i < chosenBoons.Length; i++)
      
        {
            randomBoon = Random.Range(0, availableBoons.Count);
            chosenBoons[i] = availableBoons[randomBoon];
            availableBoons.RemoveAt(randomBoon);
        }


    }
}
