using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Config")]
    [SerializeField] float health = 5000;
    [SerializeField] int scorePerEnemy = 10000;

    [Header("Gun Config")]
    [SerializeField] GameObject laserGunR;
    [SerializeField] GameObject laserGunL;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject rocketPrefab;
    float laserShotCounter;
    float rocketShotCounter;
    [SerializeField] float timeBetweenLaserShots = 3f;
    [SerializeField] float timeBetweenRocketShots = 5f;
    [SerializeField] float laserSpeed = 3f;
    [SerializeField] float rocketSpeed = 5f;

    [Header("SFX")]
    [SerializeField] AudioClip laserShootSFX;
    [SerializeField] AudioClip bombShootSFX;
    [SerializeField] AudioClip deathSFX;

    bool laser = false;
    bool pause = false;
   


    Coroutine laserCoroutine;
    EnemySpawner enemySpawner;
    Player player;


    // Start is called before the first frame update
    void Start()
    {

        rocketShotCounter = timeBetweenRocketShots;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<Player>();    
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }


    private void Fire()
    {
        if (!pause)
        {
            rocketShotCounter -= Time.deltaTime;
        }
        if (rocketShotCounter != 0 && laser == false)
        {
            StopCoroutine(LaserPause());
            laserCoroutine = StartCoroutine(LaserCoroutine());
            laser = true;
            //Debug.Log("Bool" + laser);
        }
        if (rocketShotCounter <= 0)
        {

            StopCoroutine(laserCoroutine);
            Debug.Log("laser coroutine stop");
            RocketShot();
            rocketShotCounter = timeBetweenRocketShots;
            StartCoroutine(LaserPause());
        }
    }
    IEnumerator LaserCoroutine()
    {
        //Debug.Log("laser coroutine start");
        while (true)
        {
            AudioSource.PlayClipAtPoint(laserShootSFX, Camera.main.transform.position, 0.2f);
            GameObject laserR = Instantiate(laserPrefab, laserGunR.transform.position, Quaternion.identity) as GameObject;
            GameObject laserL = Instantiate(laserPrefab, laserGunL.transform.position, Quaternion.identity) as GameObject;
            laserR.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
            laserL.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
            yield return new WaitForSeconds(timeBetweenLaserShots);
        }

    }

    IEnumerator LaserPause()
    {
        pause = true;
        yield return new WaitForSeconds(1f);
        laser = false;
        pause = false;
        //Debug.Log("Bool from coroutine" + laser);
    }

    private void RocketShot()
    {
        AudioSource.PlayClipAtPoint(bombShootSFX, Camera.main.transform.position, 0.2f);
        GameObject rocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity) as GameObject;
        //rocket.GetComponent<Rigidbody2D>().AddForce(player.transform.position * rocketSpeed);
        if (player) { rocket.GetComponent<Rigidbody2D>().velocity = player.transform.position * rocketSpeed; }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
                
        StartCoroutine(Blink());
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

     IEnumerator Blink()
     {
         GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
         yield return new WaitForSeconds(Time.deltaTime);
         GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
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

    private void Die()
    {
        Destroy(gameObject);
        enemySpawner.DecreaseEnemy();
    }
}




