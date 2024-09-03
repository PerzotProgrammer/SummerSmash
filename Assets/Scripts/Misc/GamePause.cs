using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape")) Pause();
        if (Input.GetKeyDown("backspace")) SceneManager.LoadScene("Scenes/MainMenu");
    }

    private void Pause()
    {
        if (!SceneManager.GetSceneByName("Scenes/PauseMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Scenes/UI");
            SceneManager.LoadScene("Scenes/PauseMenu", LoadSceneMode.Additive);
            Time.timeScale = 0;
        }
    }
}