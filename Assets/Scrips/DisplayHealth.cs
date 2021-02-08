using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    Player player;
    TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
