using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip bossSound;
    [SerializeField] AudioClip menuSound;
    [SerializeField] AudioClip levelSound;
    bool levels = false;
    bool boossLevel = false;
    bool startMenu = false;
    bool credits = false;
    bool rules = false;
    bool winScreen = false;
    private void Awake()
    {
        SetUpSingleton();
        

    }
    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        SwitchMusic();
    }

    private void SetUpSingleton()
    {
        
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SwitchMusic()
    {
        switch (SceneManager.GetActiveScene().name)
        {

            case "Boss Level":
                {
                    if (!boossLevel)
                    {
                        GetComponent<AudioSource>().Stop();
                        GetComponent<AudioSource>().clip = bossSound;
                        GetComponent<AudioSource>().Play();
                        GetComponent<AudioSource>().volume = 0.15f;
                        boossLevel = true;
                    }
                    break;
                }
            case "Start Menu":
                {
                    if (!startMenu)
                    {
                        GetComponent<AudioSource>().Stop();
                        GetComponent<AudioSource>().clip = menuSound;
                        GetComponent<AudioSource>().Play();
                        GetComponent<AudioSource>().volume = 0.15f;
                        startMenu = true;
                        levels = false;
                        Debug.Log(SceneManager.GetActiveScene().name);
                    }
                    break;
                }
            case "Win Screen":
                {
                    if (!winScreen)
                    {
                        GetComponent<AudioSource>().Stop();
                        GetComponent<AudioSource>().clip = menuSound;
                        GetComponent<AudioSource>().Play();
                        GetComponent<AudioSource>().volume = 0.15f;
                        winScreen = true;
                        startMenu = true;
                        Debug.Log(SceneManager.GetActiveScene().name);
                    }
                    break;
                }
           case "Rules":
                {

                    Debug.Log(SceneManager.GetActiveScene().name);
                    break; 

                }
            
                
            case "Credits":
                {

                    Debug.Log(SceneManager.GetActiveScene().name);
                    break;

                }
            default:
                {
                    if (!levels)
                    {
                        GetComponent<AudioSource>().Stop();
                        GetComponent<AudioSource>().clip = levelSound;
                        GetComponent<AudioSource>().Play();
                        GetComponent<AudioSource>().volume = 0.05f;
                        levels = true;
                        startMenu = false;
                        credits = false;
                        Debug.Log(SceneManager.GetActiveScene().name);
                    }
                    break;
                    

                }
        }
       
    }

}
