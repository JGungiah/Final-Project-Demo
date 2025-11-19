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
    private bool enemywave3;
    [Header("RootAttack")]
    public GameObject Rootfirst;
    public GameObject Rootsecond;
    public GameObject Rootthird;
    public GameObject Rootfourth;
    public float minspawnroot;
    public float maxspawnroot;

    public Animator wallAnim;

    public AudioSource vineSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yggdrasilHealth = GetComponent<Yggdrasil>();
        Enemies = GameManager.GetComponent<YggEnemySpawner>();
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
        if (yggdrasilHealth.currentHealth <= 530 && !enemywave1)
        {
            Enemies.EnemySpawn();
            enemywave1 = true;
        }
        if (yggdrasilHealth.currentHealth <= 100 && !enemywave2)
        {
            Enemies.EnemySpawn();
            enemywave2 = true;
        }
        if (yggdrasilHealth.currentHealth <= 50 && !enemywave3)
        {
            Enemies.EnemySpawn();
            enemywave3 = true;
        }
    }

    public IEnumerator RootAttack() 
    {
        StartCoroutine(yggdrasilHealth.KnockBack());
        wallAnim.SetBool("WallUp", true);
        Rootfirst.SetActive(true);
        vineSound.Play();
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootfirst.SetActive(false);
        Rootsecond.SetActive(true);
        vineSound.Play();
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootsecond.SetActive(false);
        Rootthird.SetActive(true);
        vineSound.Play();
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootthird.SetActive(false);
        Rootfourth.SetActive(true);
        vineSound.Play();
        yield return new WaitForSeconds(Random.Range(minspawnroot, maxspawnroot));
        Rootfourth.SetActive(false);
        yggdrasilHealth.isInvunrable = false;
        wallAnim.SetBool("WallUp", false);
    }
   
}
