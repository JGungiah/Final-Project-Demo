using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] public int totalCurrency;
    private int currentCurrency = 0;
    private bool hasCollected;

    [SerializeField] private TextMeshProUGUI coinsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + "" + totalCurrency.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyDrop") && !hasCollected)
        {
            hasCollected = true;
            currentCurrency = Random.Range(10, 20);
            totalCurrency += currentCurrency;
            currentCurrency = 0;
            StartCoroutine(delay());
        }

    }

    private IEnumerator delay()
    {
        yield return null;
        hasCollected = false;
    }
    
}
