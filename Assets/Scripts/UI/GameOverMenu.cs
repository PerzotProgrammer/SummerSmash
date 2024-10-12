using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private TextMeshProUGUI Score;
    private TextMeshProUGUI LoadingText;
    private GameObject HideGroup;

    private void Start()
    {
        Score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        LoadingText = GameObject.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        Score.text = $"Total kills: {EntityBase.KillCounter.ToString()}\nWave number: {WaveSystem.WaveNumber}";
        HideGroup = GameObject.Find("HideGroup");
    }

    public void RestartGame()
    {
        LoadingText.text = "Loading game.\nIt may take a while...";
        HideGroup.SetActive(false);
        SceneManager.LoadScene("Scenes/Level");
        SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/MainMenu", LoadSceneMode.Single);
    }
}