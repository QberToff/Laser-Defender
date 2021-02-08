using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //cofig parametres
    public WaveConfig waveConfig;
    public List<Transform> waypoints;
    public int waypointIndex = 0;

    //cashed references
    public EnemySpawner enemySpawner;
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    public void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;

            }
        }
        else
        {
            enemySpawner.DecreaseEnemy();
            Debug.Log("Enemy deleted");
            Destroy(gameObject);
        }
    }
}
