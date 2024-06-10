using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHpCounter : MonoBehaviour
{
    private TextMeshProUGUI HpCounter;
    private PlayerDamage PlayerDamage;

    void Start()
    {
        HpCounter = GetComponent<TextMeshProUGUI>();
        PlayerDamage = GameObject.FindWithTag("Player").GetComponent<PlayerDamage>();
    }

    void Update()
    {
        if (!PlayerDamage.IsAlive())
        {
            HpCounter.text = "";
        }
        else
        {
            HpCounter.text = $"HP: {PlayerDamage.GetHp()}";
        }
    }
}