using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    GameSession gameSession;
    TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        if (!gameSession)
        {
            Debug.Log("Can't find game session");
        }
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = gameSession.GetScore().ToString();
    }
}
