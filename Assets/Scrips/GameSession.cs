using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
    //int health = FindObjectOfType<Player>().GetHealth();
   
    
    // Start is called before the first frame update
   private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int sessionNumber = FindObjectsOfType<GameSession>().Length;
        if (sessionNumber > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);        
        }
    }

    public int ScoreIncrease(int scoreAdd)
    {
        score += scoreAdd;
        return score;
       
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }


}
