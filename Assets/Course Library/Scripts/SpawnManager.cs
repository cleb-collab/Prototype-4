using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 8;
    public GameObject powerupPrefab; 
    public int enemyCount;
    public int waveNumber = 6;
    public GameObject[] enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(6);
       Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        
    }
    void SpawnEnemyWave(int enemiestoSpawn){
        
        for (int i = 0; i< enemiestoSpawn; i++)
        {
       int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(12,0, 0);
       Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }    
    }
    private Vector3 GenerateSpawnPosition() 
    {
     float spawnPosX = Random.Range(-spawnRange,spawnRange);
     float spawnPosZ = Random.Range(-spawnRange,spawnRange);
     Vector3 randomPos = new Vector3(spawnPosX,0, spawnPosZ);
     return randomPos;
    }
   
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab,GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
   
    }
}

