using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private TextMeshProUGUI Score;

    private void Start()
    {
        Score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        Score.text += EntityBase.KillCounter.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Scenes/Level");
        SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/MainMenu", LoadSceneMode.Single);
    }
}