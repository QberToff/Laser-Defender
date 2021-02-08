using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandowFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 10;
    [SerializeField] float moveSpeed = 2f;
    bool laserPowerUp = false;
    bool healthPowerUp = false;


    public GameObject GetEnemyPrefab() { return enemyPrefab; }


    public void StartDebug()
    {
        Debug.Log("WaveConfig Start healthPowerUp " + healthPowerUp);
    }
    public List<Transform> GetWaypoints()
    {
        //Debug.Log(pathPrefab.transform.childCount);
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
            //Debug.Log(child);
        }


        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return numberOfEnemies; }

    public void LaserPowerUpOn()
    {
        laserPowerUp = true;

    }

    public void HealthPowerUpOn()
    {
        healthPowerUp = true;
    }
    public bool GetLaserPowerUp()
    {
         return laserPowerUp; 
    }

    public bool GetHealthPowerUp()
    {
        return healthPowerUp;
    }   
    public void HealthPowerUpOff()
    {
        healthPowerUp = false;
    }
    public void LaserPowerUpOff()
    {
        laserPowerUp = false;
    }




}
