using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config parametres

    [SerializeField] float health = 200;
    float startHealth;

    [Header("Power-up")]
    
    [SerializeField] float powerUpTime = 6f;
     bool laserPowerUp = false;

    [Header("Sound FX")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.3f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float lazerSoundVolume = 0.1f;
    [SerializeField] AudioClip bonusSound;
    [SerializeField] [Range(0, 1)] float bonusSoundVolume = 0.2f;


    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    

    [Header("Laser")]
    [SerializeField] List<GameObject> laserPrefab;
    [SerializeField] float laserSpeed = 15f;
    [SerializeField] float laserFirePeriod= 0.1f;

    Coroutine firingCourotine;

    float xMin; 
    float XMax;
    float yMin;
    float yMax;


    //cashed references
    Level level;

    // Start is called before the first frame update

    public float GetHealth()
    {
        return health;
    }
    
    public GameObject GetDefaultLaser()
    {
        return laserPrefab[0];
    }

    void Start()
    {

        startHealth = health;
        level = FindObjectOfType<Level>();
        SetUpMoveBoudaries();
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }


    private void SetUpMoveBoudaries()//set borders
    {

        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        XMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCourotine = StartCoroutine(FireContinuosly());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCourotine);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
           
            GameObject laser = Instantiate
                (laserPrefab[0],
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, lazerSoundVolume);
            yield return new WaitForSeconds(laserFirePeriod);
        }
        
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime *moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, XMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
        transform.position = new Vector2(newXPos, newYPos);
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        HealthDealer healthDealer = other.gameObject.GetComponent<HealthDealer>();
        LaserPowerUp laserpowerup = other.gameObject.GetComponent<LaserPowerUp>();

        if (damageDealer) { ProcessHit(damageDealer); }

        if (healthDealer) { AddHealth(healthDealer) ; }

        if (laserpowerup) { ProcessLaserPowerUp(laserpowerup); }
        
    }
    
    IEnumerator FirePowerUp()
    {

        laserPrefab.Add(laserPrefab[0]);
        laserPrefab[0] = laserPrefab[1];
        yield return new WaitForSeconds(powerUpTime);
        laserPowerUp = false;
        laserPrefab[0] = laserPrefab[2];
        laserPrefab.RemoveAt(2);
        //Debug.Log("from corotine " + laserPowerUp);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
         if (health <= 0)
        {
            health = 0;
            Die();
        }

    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        level.LoadGameOverScene();
        
    }
    
    

    private void AddHealth(HealthDealer healthDealer)
    {
        float bonus = healthDealer.GetBonusHealth();
        AudioSource.PlayClipAtPoint(bonusSound, Camera.main.transform.position, bonusSoundVolume);
        if (health + bonus > startHealth)
        {
            health = startHealth;
        }
        else
        {
            health += bonus;
        }

        healthDealer.Die();
        
    }

    private void ProcessLaserPowerUp(LaserPowerUp laserpowerup)
    {
        laserpowerup.Die();
        AudioSource.PlayClipAtPoint(bonusSound, Camera.main.transform.position, bonusSoundVolume);
        laserPowerUp = true;
        //Debug.Log(laserPowerUp);
        StartCoroutine(FirePowerUp());
    }
  
}


