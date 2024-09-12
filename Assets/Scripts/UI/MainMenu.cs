using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI Version;

    private void Start()
    {
        Version = GameObject.Find("Version").GetComponent<TextMeshProUGUI>();
        Version.text += Application.version;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Level");
        SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}