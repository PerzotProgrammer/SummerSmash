using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private bool IsGamePaused;

    private void Update()
    {
        if (Input.GetKeyDown("escape")) Pause();
        if (Input.GetKeyDown("backspace")) SceneManager.LoadScene("Scenes/MainMenu");
    }

    private void Pause()
    {
        Time.timeScale = IsGamePaused ? 1 : 0;
        IsGamePaused = !IsGamePaused;
    }
}