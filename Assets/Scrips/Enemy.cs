using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Power up config")]
    [SerializeField] GameObject[] laserPowerUps;
    [SerializeField] GameObject healthPowerUp;
    bool spawnLaserPowerUp = false;
    bool spawnHealthPowerUp = false;

    [Header("Enemy config")]
    [SerializeField] float health = 100;
    [SerializeField] int scorePerEnemy = 200;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] GameObject explosionPrefab;

    [Header("Shooting config")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 0.35f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 4f;
   
    
    
    [Header("Audio FX")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip destroySound;
    [SerializeField] [Range(0, 1)] float soundVolume = 0.3f;

    //cashed references
    EnemySpawner enemySpawner;


    public void SetHealthPowerUp()
    {
        spawnHealthPowerUp = true;
        Debug.Log("spawnHealthPowerUp " + spawnHealthPowerUp);
    }


    public float GetEnemyHealth()
    {
        return health;
    }

    public void SetLaserPowerUp()
    {

        spawnLaserPowerUp = true;
        Debug.Log("spawnLaserPowerUp " + spawnLaserPowerUp);
    }

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()//timer between shots
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, 0.2f);
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            //Debug.Log("Crashed");
            enemySpawner.DecreaseEnemy();
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
       
       health -= damageDealer.GetDamage();
       damageDealer.Hit();

       if (health <= 0)
       {
            FindObjectOfType<GameSession>().ScoreIncrease(scorePerEnemy);
            Die();
       }
        
       
    }

    private void  Die()
    {

        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, soundVolume);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        enemySpawner.DecreaseEnemy();
        //Debug.Log("Enemy killed");
        Destroy(gameObject);
        SpawnLaserPowerUp();
        SpawnHealthPowerUp();
    }

    
    private void SpawnHealthPowerUp()
    {
        if(spawnHealthPowerUp == true)
        {
            //Debug.Log("spawnHealthPowerUp " + spawnHealthPowerUp);
            GameObject healthBonus = Instantiate(healthPowerUp, 
                transform.position, 
                Quaternion.identity);
            healthBonus.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.4f, -0.8f));
            Debug.Log("health spawned");
            spawnHealthPowerUp = false;
        }
    }


    private void SpawnLaserPowerUp()
    {
        
        if (spawnLaserPowerUp)
        {
           
            GameObject laserPowerUp = Instantiate(laserPowerUps[Random.Range(0, laserPowerUps.Length)],
               transform.position,
               Quaternion.identity);
            laserPowerUp.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.4f, -0.8f));
            spawnLaserPowerUp = false;
        }
    }

   
}

