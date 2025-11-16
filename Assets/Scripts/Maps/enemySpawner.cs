using System.Collections;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public Transform[] SpawnLocations;


    public GameObject[] Enemies;
    public int Count;
    public GameObject[] enemyCount;
    public GameObject gate;
   
    public bool wave1;
    public bool wave2;
    public bool wavescomplete;
    public bool wavescompletecheck;
    public static float numberOfWavesCompleted = 0;


    private Quaternion spawnRotation = Quaternion.Euler(0f, 45f, 0f);

    public bool WaveEnded;
    private void Start()
    {
     
        if (Count == 0 )
        {
            Wave1Spawn();
        }

    }

    void Update()
    {
       
        countenemies();

        if (wavescomplete && !wavescompletecheck) 
        {
            StartCoroutine(waitabit());
           
        }
      
    }
    public void Wave1Spawn() 
    {
        for (int i = 0; i < SpawnLocations.Length; i++) 
        {
        int randomIndex = Random.Range(0, Enemies.Length);
        GameObject chosenEnemy = Enemies[randomIndex];
        Instantiate(chosenEnemy, SpawnLocations[i].position, spawnRotation);
         
        }
        wave1 = true;
        numberOfWavesCompleted++;
    }
    IEnumerator waitabit() 
    {
        WaveEnded = false;
        yield return new WaitForSeconds(1f);
        gate.SetActive(true);
        wavescompletecheck = true;
    }

  

    public void Wave2Spawn()
    {
       
        for (int i = 0; i < SpawnLocations.Length; i++)
        {
            int randomIndex = Random.Range(0, Enemies.Length);
            GameObject chosenEnemy = Enemies[randomIndex];
            Instantiate(chosenEnemy, SpawnLocations[i].position, spawnRotation);
           
        }
       wave2 = true;
        
    }

 
    public void countenemies() 
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        Count = enemyCount.Length;

        if(Count == 0 && wave1) 
        {
            wave1 = false;
            StartCoroutine(waveinterem());
        }
        if (Count == 0 && wave2)
        {
            wave2 = false;
            wavescomplete = true;
           
        }
    }


    IEnumerator waveinterem() 
    {
        WaveEnded = true;
        numberOfWavesCompleted++;
        yield return new WaitForSeconds(3f);
            Wave2Spawn();
    }
}
