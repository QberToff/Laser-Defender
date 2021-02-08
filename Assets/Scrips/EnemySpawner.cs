//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loopipng = false;
    [SerializeField] int enemyCounter = 0;
    [SerializeField] int allEnmies = 0;

    int waveIndex;
    int laserSpawnChance = 75;
    int healthSpawnChance = 50;

    //cashed references
    Level level;
    Player player;

    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Debug.Log(waveConfigs.Count);
        player = FindObjectOfType<Player>();
        level = FindObjectOfType<Level>();
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (loopipng);
    }

    private void Update()
    {
        if (enemyCounter == allEnmies && waveIndex == waveConfigs.Count)
        {
            level.LoadGameScene();
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            waveConfigs[waveIndex].LaserPowerUpOff();
            waveConfigs[waveIndex].HealthPowerUpOff();
            ProcessLaserBonus();
            ProcessHealthBonus();
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }


    }

    private void ProcessLaserBonus()//spawn bonus with probability = laserSpawnChance 
    {
        System.Random r = new System.Random();
        int currentTry = r.Next(100);
        if (currentTry < laserSpawnChance)
        {

            SpawnLaserBonus();
        }
        else { waveConfigs[waveIndex].LaserPowerUpOff(); }
        
    }
    private void ProcessHealthBonus()//spawn bonus with probability = healthSpawnChance 
    {
        System.Random r = new System.Random();
        int currentTry = r.Next(100);
        if (currentTry < healthSpawnChance)
        {

            SpawnHealthBonus();
            
        }
        else { waveConfigs[waveIndex].LaserPowerUpOff(); }
    }

    private void SpawnLaserBonus()//if player can't kill enemies in next wave with one shot spawn laser power-up
    {
        if (waveIndex + 1 != waveConfigs.Count &&
         waveConfigs[waveIndex + 1].GetEnemyPrefab().GetComponent<Enemy>().GetEnemyHealth() >
        player.GetDefaultLaser().GetComponent<DamageDealer>().GetDamage())
        {
          waveConfigs[waveIndex].LaserPowerUpOn();
        }
        
    }

    private void SpawnHealthBonus()
    {
        if(player.GetHealth() <= 750f)
        {
            waveConfigs[waveIndex].HealthPowerUpOn();
            Debug.Log("palyer health less 750hp");
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        int laserrnd = Random.Range(0, waveConfig.GetNumberOfEnemies() - 1);//choose random enemy to spawn laser power-up
        int healthrnd = Random.Range(0, waveConfig.GetNumberOfEnemies() - 1);//choose random enemy to spawn laser power-up
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            allEnmies++;

            if (waveConfig.GetLaserPowerUp() && enemyCount == laserrnd)
            {
                newEnemy.GetComponent<Enemy>().SetLaserPowerUp();
            }

            if (waveConfig.GetHealthPowerUp() && enemyCount == healthrnd)
            {
                Debug.Log("Waveconfig " + waveConfig.GetHealthPowerUp());
                newEnemy.GetComponent<Enemy>().SetHealthPowerUp();
            }
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }

    public void DecreaseEnemy()
    {
        enemyCounter++;
    }
   
    
}
