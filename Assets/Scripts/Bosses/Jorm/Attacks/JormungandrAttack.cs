using System.Collections;
using UnityEngine;

public class JormungandrAttack : MonoBehaviour
{
  private Jormungandr JormHealth;
  public GameObject GameManager;
  private JormenemySpawner Enemies;

    [Header("Spit attack")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    private GameObject player;
    [SerializeField] private float projectileSpeed;


    [Header("Rock Fall")]
    public GameObject rockPrefab;
    public Transform[] spawnLocations;
    [SerializeField] private float numrocks;
    [SerializeField]private float minSpawn = 0.2f;
    [SerializeField]private float maxSpawn = 0.7f;


    [Header("Enemies Spawning")]
    private bool enemywave1;
    private bool enemywave2;
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        JormHealth = GetComponent<Jormungandr>();
        GameManager = GameObject.FindWithTag("RuneManager");
        Enemies = GameManager.GetComponent<JormenemySpawner>();
        
    }
    public void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Vector3 direction = (player.transform.position - firePoint.position).normalized;
        projectile.transform.forward = direction;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 force = direction * projectileSpeed;
        rb.AddForce(force,ForceMode.Impulse);
    }

    
    
   public IEnumerator StartFalling() 
    {
        for (int i = 0; i <= 45; i++)
        {

            Transform points = spawnLocations[Random.Range(0, spawnLocations.Length)];
            Instantiate(rockPrefab, points.position, points.rotation);
            yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        }
        JormHealth.isInvunrable = false;
    }

    public void EnemySpawner() 
    {
        if(JormHealth.currentHealth <= 600 && !enemywave1) 
        {
            Enemies.EnemySpawn();
            enemywave1 = true;
        }
        if(JormHealth.currentHealth <= 300 && !enemywave2) 
        {
            Enemies.EnemySpawn();
            enemywave2 = true;
        }
    }

   

}
