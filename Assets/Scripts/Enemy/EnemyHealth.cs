using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Attack attackScript;
    [SerializeField] private float maxHealth;
    public float currentHealth;
    [SerializeField]  private float playerDamage;
    private bool canTakeDamage = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !canTakeDamage)
        {
            currentHealth -= playerDamage;
            canTakeDamage = true;
            StartCoroutine(DamageWindow());
        }
    }

    IEnumerator DamageWindow()
    {
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = false;
    }

}
