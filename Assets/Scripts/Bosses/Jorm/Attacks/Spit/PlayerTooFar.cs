using UnityEngine;

public class PlayerTooFar : MonoBehaviour
{
    public JormungandrAttack FireProj;
    public GameObject Jorm;
    public Jormungandr JormHealth;
    public float enemyhealth;
    public float timer;
    public float intervaltimer = 10f;
    private void Start()
    {
        FireProj = Jorm.GetComponent<JormungandrAttack>();
        JormHealth = Jorm.GetComponent<Jormungandr>();
    }

    private void Update()
    {
     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            FireProj.RangedAttack();
        }
    }
    public void PlayerAttack() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        if (JormHealth.currentHealth == enemyhealth)
        {
            FireProj.RangedAttack();
        }
        enemyhealth = JormHealth.currentHealth;
        timer = intervaltimer;
    }
}
