using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public UIManager uiManager;


    private float maxSpawnRangeZ = 25;
    private float minimumSpawnRangeZ = 10;
    private float enemySpawnRangeX = 30.0f;
    private float powerUpSpawnRange = 5.0f;
    
    private float startPowerUpSpawn = 5f;
    private float powerUpSpawnInterval = 15f;

    public int waveNum = 1;
    public int enemyNum = 4;
    public int currentRound = 1;
    public int enemyCount;
    public int powerUpCount;

    public GameObject bossEnemy;
    public int bossCount;
    private int bossRound = 5;
    public bool isBossRound = false;

    public bool isGameActive;
    // Start is called before the first frame update

    void Start()
    {
        //Imports the UIManager script
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        isGameActive = uiManager.isGameActive;

        //Spawns first wave of enemies if the game is active
        if (isGameActive == true)
        {
            //Calls SpawnEnemyWave method 
            SpawnEnemyWave(enemyNum);
            
           
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Counts how many enemies are currenlty spawned
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //Counts how many powerups are currenlty spawned
        powerUpCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        //Counts how many bosses are currenlty spawned
        bossCount = GameObject.FindGameObjectsWithTag("Boss").Length;
        //Checks if the game is active before spawning enemies
        if(isGameActive == true) {
            //Repeatedly Spawns powerups  

            SpawnPowerUp();

            //Spawns a new wave when the enemy count and boss count reach 0
            if (enemyCount == 0 && bossCount == 0)
            {
            //Increases the wave number by 1 when enemy count reaches 0
            waveNum++;

            isBossRound = false;
            
            SpawnEnemyWave(enemyNum);
            }
        
       //When the boss isn't spawned and the round is a multiple of 5 it will spawn
       if(!isBossRound && waveNum % bossRound == 0)
        {
            SpawnBossRound(1);
            //Indicates if a boss has spawned 
            isBossRound = true;       
        }
    }

}
    //Generates a random spawn position for enemy prefabs
    private Vector3 GenerateEnemySpawnPos()
    {

        float spawnPosX = Random.Range(-enemySpawnRangeX,enemySpawnRangeX);
        float spawnPosZ = Random.Range(minimumSpawnRangeZ, maxSpawnRangeZ);

        Vector3 randomPos = new Vector3(spawnPosX, 0.5f , spawnPosZ);
        return randomPos;
    }
    //Generates random spawns for powerups
    private Vector3 GeneratePowerUpSpawnPos()
    {
        float spawnPosX = Random.Range(-powerUpSpawnRange,powerUpSpawnRange);
        float spawnPosZ = Random.Range(-powerUpSpawnRange, powerUpSpawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0.3f, spawnPosZ);
        return randomPos;
    }
    //Method for spawning enemy waves
    void SpawnEnemyWave(int enemiesToSpawn) 
    {
        //The enemies spawn and the amount depends on the wave number
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateEnemySpawnPos() , enemyPrefab.transform.rotation);
        }
        //Increases the enemies to spawn everytime a round ends
        enemyNum++;

    }
    //Spawns a powerup if the powerup count is 0
    void SpawnPowerUp()
    {
        if(powerUpCount == 0 && enemyCount == 0) { 
        Instantiate(powerUpPrefab, GeneratePowerUpSpawnPos(), powerUpPrefab.transform.rotation);
        }
    }

    //Method for spawning a boss everytime it is called in void update
    public void SpawnBossRound(int bossNum)
    {
        
      for(int i = 0; i< bossNum; i++){
         Instantiate(bossEnemy, GenerateEnemySpawnPos(), bossEnemy.transform.rotation);
      }
        
        
    }
    //Sets the game to active when the start button is clicked
    public void StartGame()
    {
        isGameActive = true;
       
    }
}