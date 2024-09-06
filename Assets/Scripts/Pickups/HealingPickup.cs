using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickup : PickupBase
{
    [SerializeField] private int healedHp;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EntityBase entityBase = other.GetComponent<EntityBase>();
        if (entityBase && !entityBase.HasMaxHp()) Pickup(entityBase);
    }

    protected override void Pickup(EntityBase entityBase)
    {
        entityBase.HealHp(healedHp);
        Destroy(gameObject);
    }
}