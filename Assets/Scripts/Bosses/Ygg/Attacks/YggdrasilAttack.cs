using System.Collections;
using UnityEngine;

public class YggdrasilAttack : MonoBehaviour
{
    [Header("Rock Fall")]
    public GameObject rockPrefab;
    public Transform[] spawnLocations;

    [SerializeField] private float minSpawn = 0.1f;
    [SerializeField] private float maxSpawn = 0.3f;

    public GameObject GameManager;
    private YggEnemySpawner Enemies;
    private Yggdrasil yggdrasilHealth;


    [Header("Enemies Spawning")]
    private bool enemywave1;
    private bool enemywave2;

    [Header("RootAttack")]
    public GameObject Rootfirst;
    public GameObject Rootsecond;
    public GameObject Rootthird;
    public GameObject Rootfourth;
    public float minspawnroot;
    public float maxspawnroot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yggdrasilHealth = GetComponent<Yggdrasil>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawner();
    }
    public IEnumerator StartFalling()
    {
        for (int i = 0; i <= 45; i++)
        {

            Transform points = spawnLocations[Random.Range(0, spawnLocations.Length)];
            Instantiate(rockPrefab, points.position, points.rotation);
            yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
         
        }
        yggdrasilHealth.isInvunrable = false;
    }

    public void EnemySpawner()
    {
        if (yggdrasilHealth.currentHealth <= 850 && !enemywave1)
        {
            Enemies.EnemySpawn();
            enemywave1 = true;
        }
        if (yggdrasilHealth.currentHealth <= 400 && !enemywave2)
        {
            Enemies.EnemySpawn();
            enemywave2 = true;
        }
    }

    public IEnumerator RootAttack() 
    {
        Rootfirst.SetActive(true);
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootfirst.SetActive(false);
        Rootsecond.SetActive(true);
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootsecond.SetActive(false);
        Rootthird.SetActive(true);
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootthird.SetActive(false);
        Rootfourth.SetActive(true);
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootfourth.SetActive(false);
        yggdrasilHealth.isInvunrable = false;
    }
   
}
