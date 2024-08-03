using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickup : PickupBase
{
    [SerializeField] private int healedHp;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !PlayerLogic.HasMaxHp()) Pickup();
    }

    private void Pickup()
    {
        PlayerLogic.HealHp(healedHp);
        Destroy(gameObject);
    }
}