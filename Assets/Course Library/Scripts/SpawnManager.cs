using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 8;
    public GameObject powerupPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(3);
       Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        
    }
    void SpawnEnemyWave(int enemiestoSpawn){
        for (int i = 0; i< enemiestoSpawn; i++)
      {
       Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
      }    
    }
    private Vector3 GenerateSpawnPosition() {
     float spawnPosX = Random.Range(spawnRange,spawnRange);
     float spawnPosZ = Random.Range(spawnRange,spawnRange);
     Vector3 randomPos = new Vector3(spawnPosX,0, spawnPosZ);
     return randomPos;
    }
    // Update is called once per frame
    void Update()
    {
 
 {
            Instantiate(powerupPrefab,GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
}
