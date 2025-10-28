using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RandomizeBoons : MonoBehaviour
{
    public UpgradeScriptableObjects[] Boons;
    public UpgradeScriptableObjects[] chosenBoons = new UpgradeScriptableObjects[3];

    private List<UpgradeScriptableObjects> availableBoons;


    [SerializeField] private TextMeshProUGUI[] boonNamesUI = new TextMeshProUGUI[3];
    [SerializeField] private Image[] boonImages = new Image[3]; 
    [SerializeField] private TextMeshProUGUI[] boonDescriptionsUI = new TextMeshProUGUI[3];

    private string[] boonNames;
    private string[] boonDescriptions;

    [SerializeField] GameObject boonCanvas;

    private GameObject player;
    private Transform canvas;

    public UpgradeScriptableObjects selectedBoon;

    public bool isActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player");
     
        if (player != null)
        {
            canvas = player.transform.Find("Canvas");
        }

        RandomizeStatBoons();
        AssignUIValues();

    }

    // Update is called once per frame
    void Update()
    {
        if (boonCanvas.activeSelf)
        {  
                canvas.gameObject.SetActive(false);
              
        }
    }


    private UpgradeScriptableObjects WeightedBoon( List<UpgradeScriptableObjects> boons)
    {
        float totalChance = 0;

        foreach (var boon in boons)
        {
            totalChance += boon.GetWeighting();
        }

        float randomChance = Random.Range(0, totalChance);
        float cummulativeChance = 0;

        foreach (var boon in boons)
        {
            cummulativeChance += boon.GetWeighting();

            if (randomChance <= cummulativeChance)
            {
                return boon;
            }
        }
        return boons[0];
    }

    public void RandomizeStatBoons()
    {
        availableBoons = new List<UpgradeScriptableObjects> (Boons);

        boonNames = new string[chosenBoons.Length];
        boonDescriptions = new string[chosenBoons.Length];

        for (int i = 0; i < chosenBoons.Length; i++)
      
        {

            UpgradeScriptableObjects weightedBoons = WeightedBoon(availableBoons);
            chosenBoons[i] = weightedBoons;
            availableBoons.Remove(weightedBoons);  
            
        }


    }

    public void AssignUIValues()
    {
        for (int i = 0; i < chosenBoons.Length; i++)
        {
            boonNames[i] = chosenBoons[i].GetBoonName();
            boonNamesUI[i].text = boonNames[i];
            boonDescriptions[i] = chosenBoons[i].GetBoonDescription();
            boonDescriptionsUI[i].text = boonDescriptions[i];
            boonImages[i].sprite = chosenBoons[i].GetBoonImage();

        }
    }

    public void SelectBoon(int boonIndex)
    {
        selectedBoon = chosenBoons[boonIndex];


        Health playerHealth = player.GetComponent<Health>();
        Player playerMovement = player.GetComponent<Player>();
        Attack playerAttack = player.GetComponent<Attack>();

        playerHealth.ApplyHealthBoon(selectedBoon);
        playerMovement.ApplyMovementBoon(selectedBoon); 
        playerAttack.ApplyAttackBoon(selectedBoon); 
     

        boonCanvas.SetActive(false);
        canvas.gameObject.SetActive(true);
        isActive = true;
    }

    

}
