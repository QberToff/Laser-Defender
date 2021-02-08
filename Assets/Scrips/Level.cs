using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    public void LoadGameScene()
    {
        StartCoroutine(GameScene());
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadGameOver());
    }

    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene(0);
        if(SceneManager.GetActiveScene().name != "Credits" || SceneManager.GetActiveScene().name != "Rules") FindObjectOfType<GameSession>().ResetScore();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadRules()
    {
        SceneManager.LoadScene("Rules");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

   IEnumerator GameScene()
    {
        yield return new WaitForSeconds(loadDelay);
        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            SceneManager.LoadScene(1);
            FindObjectOfType<GameSession>().ResetScore();
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene("Game Over");

    }
    
    

}


