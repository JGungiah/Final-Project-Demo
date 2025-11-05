using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private GameObject player;
    private Attack playerAttack;
    private TMP_Text text;
    public GameObject numbers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<Attack>();
        text = GetComponent<TMP_Text>();
        Destroy(numbers, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = playerAttack.playerDamage.ToString();
    }
}
