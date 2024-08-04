using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool IsGamePaused;

    private void Update()
    {
        // SKRYPT PRZYPIĘTY DO SPAWNERA    
        if (Input.GetKeyDown("escape")) Pause();
    }

    private void Pause()
    {
        Time.timeScale = IsGamePaused ? 1 : 0;
        IsGamePaused = !IsGamePaused;
    }
}