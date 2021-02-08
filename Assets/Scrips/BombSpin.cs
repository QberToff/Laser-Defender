using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpin : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 1f;
    //Player player;
    //[SerializeField] float speed = 2f;
    //Rigidbody2D rb;


    private void Awake()
    {
        //layer = FindObjectOfType<Player>();
        //rb = GetComponent<Rigidbody2D>();
        //Debug.Log(player.transform.position);
    }
    // Update is called once per frame

    private void Start()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.05f);
    }
    void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
        //ToPlayer();
        


    }

}
