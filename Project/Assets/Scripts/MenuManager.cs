using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void FromMenuToNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("NewGame");
    }

    public void FromMenuToGrove()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Grove");
    }

    public void FromMenuToOptions()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Options");
    }
    public void FromMenuToCredits()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Credits");
    }
    public void FromNewGameToPhase()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Phase0");
    }

    public void FromNewGameToTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    public void FromTutorialToPhase()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Phase0");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void BackToOptions()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}