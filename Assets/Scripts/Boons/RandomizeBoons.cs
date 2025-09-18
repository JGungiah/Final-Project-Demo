using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RandomizeBoons : MonoBehaviour
{
    public UpgradeScriptableObjects[] StatusBoons;
    public UpgradeScriptableObjects[] chosenBoons = new UpgradeScriptableObjects[3];

    private List<UpgradeScriptableObjects> availableBoons;
   
    private int randomBoon;

    [SerializeField] private TextMeshProUGUI[] boonNamesUI = new TextMeshProUGUI[3];
    [SerializeField] private Image[] boonImages = new Image[3]; 
    [SerializeField] private TextMeshProUGUI[] boonDescriptionsUI = new TextMeshProUGUI[3];

    private string[] boonNames;
    private string[] boonDescriptions;

    [SerializeField] GameObject boonCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeStatBoons();
        AssignUIValues();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void RandomizeStatBoons()
    {
        availableBoons = new List<UpgradeScriptableObjects> (StatusBoons);

        boonNames = new string[chosenBoons.Length];
        boonDescriptions = new string[chosenBoons.Length];

        for (int i = 0; i < chosenBoons.Length; i++)
      
        {
            randomBoon = Random.Range(0, availableBoons.Count);
            chosenBoons[i] = availableBoons[randomBoon];
            availableBoons.RemoveAt(randomBoon);
        }


    }

    void AssignUIValues()
    {
        for (int i = 0; i < chosenBoons.Length; i++)
        {
            boonNames[i] = chosenBoons[i].GetBoonName();
            boonNamesUI[i].text = boonNames[i];
            boonDescriptions[i] = chosenBoons[i].GetBoonDescription();
            boonDescriptionsUI[i].text = boonDescriptions[i];   
            //boonImages[i].sprite = chosenBoons[i].GetBoonImage().sprite;
            
        }
    }

   
}
