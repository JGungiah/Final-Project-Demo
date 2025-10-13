using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float fullHealthCost;
    [SerializeField] private float smallhealthCost;
    [SerializeField] private float boonCost;

    private GameObject player;
    private PlayerCollect collectScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
            print(1);
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
            print(2);
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
            print(3);
        }
        else
        {
            print("You do not have enough currency");
        }
    }

  
}
