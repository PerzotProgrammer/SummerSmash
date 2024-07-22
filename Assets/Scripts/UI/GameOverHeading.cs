using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private TextMeshProUGUI GameOverHeading;
    private PlayerLogic PlayerLogic;

    private void Start()
    {
        GameOverHeading = GetComponent<TextMeshProUGUI>();
        PlayerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
    }

    private void Update()
    {
        if (!PlayerLogic.IsAlive()) GameOverHeading.text = "game over christopher";
    }
}