using System.Collections;
using UnityEngine;

public class JormEnemySpawner : MonoBehaviour
{
    public Transform[] SpawnLocations;
    public GameObject[] Enemies;
    private Quaternion spawnRotation = Quaternion.Euler(0f, 45f, 0f);

   
    public void EnemySpawn() 
    {
      
        for (int i = 0; i < SpawnLocations.Length; i++) 
        {
        int randomIndex = Random.Range(0, Enemies.Length);
        GameObject chosenEnemy = Enemies[randomIndex];
        Instantiate(chosenEnemy, SpawnLocations[i].position, spawnRotation);        
        }

      
    }


   

  


    
}
