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
    private bool ChangeTextColor;
    private bool ColorBoolean;

    private void Start()
    {
        Version = GameObject.Find("Version").GetComponent<TextMeshProUGUI>();
        LoadingText = GameObject.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        HideGroup = GameObject.Find("HideGroup");
        Version.text += Application.version;
        ChangeTextColor = true;
    }

    private void Update()
    {
        if (ChangeTextColor) StartCoroutine(nameof(ChangeTextColorCoroutine));
    }

    private IEnumerator ChangeTextColorCoroutine()
    {
        ChangeTextColor = false;
        Version.color = ColorBoolean ? new Color(1.0f, 0.2f, 0.2f) : Color.white;
        ColorBoolean = !ColorBoolean;
        yield return new WaitForSeconds(0.5f);
        ChangeTextColor = true;
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