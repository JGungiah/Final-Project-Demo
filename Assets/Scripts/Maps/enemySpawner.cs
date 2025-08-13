using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public Transform[] SpawnLocations;
    public GameObject[] Enemies;
    public int count;

 
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Wave1Spawn() 
    {
        
        for (int i = 0; i < SpawnLocations.Length; i++) 
        {
        int randomIndex = Random.Range(0, Enemies.Length);
        GameObject chosenEnemy = Enemies[randomIndex];
        Instantiate(chosenEnemy, SpawnLocations[i].position, SpawnLocations[i].rotation);
            count++;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Wave1Spawn();
        }
        
    }
}
