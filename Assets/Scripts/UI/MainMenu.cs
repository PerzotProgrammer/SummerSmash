using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/UI");
        SceneManager.LoadScene("Scenes/Lvl0", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}