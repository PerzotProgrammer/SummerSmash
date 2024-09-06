using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider Slider;
    private EntityBase EntityBase;

    private void Start()
    {
        Slider = GetComponentInChildren<Slider>();
        EntityBase = GetComponentInParent<EntityBase>();
        if (!EntityBase)
        {
            EntityBase = FindPlayer();
            EntityBase.GetComponent<PlayerLogic>().SetHealthBar(this);
        }
        else HideHealthBar();
    }

    private void HideHealthBar()
    {
        Slider.gameObject.SetActive(false);
    }

    private void ShowHealthBar()
    {
        Slider.gameObject.SetActive(true);
    }

    private EntityBase FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<EntityBase>();
    }

    public void UpdateHealthBar()
    {
        Slider.value = (float)EntityBase.GetHp() / EntityBase.GetMaxHp();
        if (EntityBase.HasMaxHp()) HideHealthBar();
        else ShowHealthBar();
    }
}