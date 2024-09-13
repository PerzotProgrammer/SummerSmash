using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class OnlyForEditor : MonoBehaviour
{
#if UNITY_EDITOR
    // !!! KOD TYLKO DO UŻYWANIA W EDYTORZE 
    // NIE WPŁYWA NA KOMPILACJE

    [SerializeField] private bool invincibility;
    [SerializeField] private bool unlimitedAmmunition;
    [SerializeField] private bool resetAmmunition;
    private PlayerLogic PlayerLogic;

    private void Start()
    {
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        if (unlimitedAmmunition)
        {
            for (int i = 0; i < WeaponLogic.MagazineState.Length; i++)
            {
                WeaponLogic.MagazineState[i] = int.MaxValue;
            }
        }

        LoadUIIfNotLoaded();
    }

    private void Update()
    {
        if (invincibility && !PlayerLogic.HasMaxHp()) PlayerLogic.HealHp(PlayerLogic.GetMaxHp());
        if (resetAmmunition)
        {
            for (int i = 0; i < WeaponLogic.MagazineState.Length; i++)
            {
                WeaponLogic.MagazineState[i] = -1;
            }

            resetAmmunition = false;
        }
    }

    private void LoadUIIfNotLoaded()
    {
        if (!SceneManager.GetSceneByName("Scenes/UI").isLoaded)
        {
            SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
        }
    }


#endif
}