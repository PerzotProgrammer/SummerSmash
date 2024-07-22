using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHpCounter : MonoBehaviour
{
    private TextMeshProUGUI HpCounter;
    private PlayerLogic PlayerLogic;

    private void Start()
    {
        HpCounter = GetComponent<TextMeshProUGUI>();
        PlayerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
    }

    private void Update()
    {
        HpCounter.text = !PlayerLogic.IsAlive() ? "" : $"HP: {PlayerLogic.GetHp()}";
    }
}