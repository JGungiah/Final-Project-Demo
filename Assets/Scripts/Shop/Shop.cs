using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private int fullHealthCost;
    [SerializeField] private int smallhealthCost;
    [SerializeField] private int boonCost;

    private GameObject player;
    private PlayerCollect collectScript;
    private Health healthScript;

    [SerializeField] private float smallHealthAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        healthScript = player.GetComponent<Health>();
        collectScript = player.GetComponent<PlayerCollect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fullHealth()
    {
        if (collectScript.totalCurrency > fullHealthCost)
        {
           healthScript.currentHealth = healthScript.originalMaxHealth;
            collectScript.totalCurrency -= fullHealthCost;
        }
        else
        {
            print("You do not have enough currency");
        }
    }

    public void smallHealth()
    {
        if (collectScript.totalCurrency > smallhealthCost)
        {
            healthScript.currentHealth += smallHealthAmount;
            collectScript.totalCurrency -= smallhealthCost;
        }
        else
        {
            print("You do not have enough currency");
        }
    }

    public void Boon()
    {
        if (collectScript.totalCurrency > boonCost)
        {
            collectScript.totalCurrency -= boonCost;
        }
        else
        {
            print("You do not have enough currency");
        }
    }

  
}
