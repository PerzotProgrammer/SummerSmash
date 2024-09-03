using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeCurrentGame()
    {
        SceneManager.UnloadSceneAsync("Scenes/PauseMenu");
        SceneManager.LoadSceneAsync("Scenes/UI", LoadSceneMode.Additive);
        Time.timeScale = 1;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}