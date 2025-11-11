using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingNumbers : MonoBehaviour
{
    public TextMeshProUGUI scrollingNum;

    public float spinTime;

    public bool canSpin;
    private bool hasGivenhealth;
    public GameObject button;
    public Button spinButton;
    private int randomValue1;

    private Color color;
    private GameObject player;
    private PlayerCollect PlayerCollect;
    private Health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PlayerCollect = player.GetComponent<PlayerCollect>();
        health = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (canSpin )
        {
            StartCoroutine(cycleNumbers());
             randomValue1 = Random.Range(15, 30);
            scrollingNum.text = randomValue1.ToString(); 
        }

        if (PlayerCollect.totalCurrency < 50)
        {
            spinButton.enabled = false;

            Color buttonColor = spinButton.image.color;
            buttonColor.a = 0.2f; 
            spinButton.image.color = buttonColor;
        }
        else if (PlayerCollect.totalCurrency >= 50)
        {
            spinButton.enabled = true;

            Color buttonColor = spinButton.image.color;
            buttonColor.a = 1f; 
            spinButton.image.color = buttonColor;
        }

    }

    public void SPIN()
    {
        canSpin = true;
        button.SetActive(false);
        PlayerCollect.totalCurrency -= 50;
    }

    IEnumerator cycleNumbers()
    {
        yield return new WaitForSeconds(spinTime);
        canSpin = false;

        if (!hasGivenhealth)
        {
            health.currentHealth += randomValue1;
            hasGivenhealth = true;
        }
       
        
      
    }
}
