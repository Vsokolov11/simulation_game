using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
