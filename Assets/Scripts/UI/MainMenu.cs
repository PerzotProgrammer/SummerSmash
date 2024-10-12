using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI Version;
    private TextMeshProUGUI LoadingText;
    private GameObject HideGroup;

    private void Start()
    {
        Version = GameObject.Find("Version").GetComponent<TextMeshProUGUI>();
        LoadingText = GameObject.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        HideGroup = GameObject.Find("HideGroup");
        Version.text += Application.version;
    }

    public void PlayGame()
    {
        LoadingText.text = "Loading game.\nIt may take a while...";
        HideGroup.SetActive(false);
        SceneManager.LoadScene("Scenes/Level");
        SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}