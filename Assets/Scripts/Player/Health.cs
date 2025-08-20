using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthDecreaseSpeed;
    [SerializeField] private Image healthBar;
    private float elapsedTime;
    public float currentHealth;


    public bool hasBeenAttacked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage (float damage)
    {
       
        elapsedTime += Time.deltaTime;
        currentHealth -= damage ;
        //if (elapsedTime > healthDecreaseSpeed) ;
        
    }

    
}
