using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPickup : PickupBase
{
    [SerializeField] private int duration;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EntityBase entityBase = other.GetComponent<EntityBase>();
        if (entityBase && !entityBase.IsOnSpeedUp) Pickup(entityBase);
    }

    protected override void Pickup(EntityBase entityBase)
    {
        entityBase.SpeedUp(duration);
        base.Pickup(null);
    }
}