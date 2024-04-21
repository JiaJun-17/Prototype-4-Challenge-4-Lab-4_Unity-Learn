using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //need a reference to enemy prefab
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public int enemyCount;
    public int waveNumber = 1; //number of enemy should be spawn
    private float spawnRangeXRight = 7.31f;
    private float spawnRangeXLeft = -7.0f;
    private float spawnRangeZTop = 11.0f;
    private float spawnRangeZBottom = -10.5f;


    // Start is called before the first frame update
    void Start()
    {  
        //when start, there will be a enemy and powerup
        //3 enemies
        SpawnEnemyWave(waveNumber); //need a number to start
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;    //use Enemy's script
        if(enemyCount == 0){
            //1 enemy-> 2 enemies-> 3 enemies ...
            waveNumber++; //when all enemy defeated,increase waveNumer by 1
            SpawnEnemyWave(waveNumber);
            //when new wave created, new powerup created 
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
    }

    //create different numbers of enemies
    void SpawnEnemyWave(int enemiesToSpawn){  //(parameters)
        for(int i=0 ; i<enemiesToSpawn ; i++){ //i=i+1 or i+=1 or i++
        //enemyPrefab.transform.rotation is to make sure the orientation is correct
        //GenerateSpawnPosition() to get the random position
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation); 
        }
    }

    //private void GenerateSpawnPosition, if using void, doesn't return value
    //provide the random position (Vector3 randomPosition) that we need to use, so use Vector3
    private Vector3 GenerateSpawnPosition(){    //custom method with return value that we need to use 

        float spawnPositionX = Random.Range(spawnRangeXLeft,spawnRangeXRight);
        float spawnPositionZ = Random.Range(spawnRangeZBottom,spawnRangeZTop);
        
        Vector3 randomPosition = new Vector3(spawnPositionX,0,spawnPositionZ);
        return randomPosition;

    }

}
