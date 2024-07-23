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
    }

    public void UpdateHealthBar()
    {
        Slider.value = (float)EntityBase.GetHp() / EntityBase.GetMaxHp();
    }

    public void HideHealthBar()
    {
        gameObject.SetActive(false);
    }

    public void ShowHealthBar()
    {
        gameObject.SetActive(true);
    }
}