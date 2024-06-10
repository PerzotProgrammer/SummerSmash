using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private TextMeshProUGUI GameOverHeading;
    private PlayerDamage PlayerDamage;

    void Start()
    {
        GameOverHeading = GetComponent<TextMeshProUGUI>();
        PlayerDamage = GameObject.FindWithTag("Player").GetComponent<PlayerDamage>();
    }

    void Update()
    {
        if (!PlayerDamage.IsAlive())
        {
            GameOverHeading.text = "game over christopher";
        }
    }
}