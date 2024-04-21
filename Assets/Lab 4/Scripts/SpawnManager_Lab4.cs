using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Lab4 : MonoBehaviour
{
    public GameObject[] enemies; //store the prefabs
    public GameObject powerUp;

    private float zEnemySpawn = 6.0f;
    private float zPowerUpRange = 3.0f;
    private float xSpawnRangeLeft = -6.0f;
    private float xSpawnRangeRight = 7.0f;
    private float ySpawn = 0.3f;

    private float powerUpSpawnTime = 5.0f;
    private float enemySpawnTime = 1.0f;
    private float startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerUp", startDelay, powerUpSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomEnemy(){
        float randomX = Random.Range(xSpawnRangeLeft, xSpawnRangeRight);
        int randomIndex = Random.Range(0, enemies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
    }

    void SpawnPowerUp(){
        float randomX = Random.Range(xSpawnRangeLeft, xSpawnRangeRight);
        float randomZ = Random.Range(-zPowerUpRange, zPowerUpRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);

        Instantiate(powerUp, spawnPos, powerUp.gameObject.transform.rotation);
    }
}
