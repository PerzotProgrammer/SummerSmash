using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPickup : PickupBase
{
    [SerializeField] private int duration;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !PlayerLogic.IsOnSpeedBoost()) Pickup();
    }

    private void Pickup()
    {
        PlayerLogic.SpeedUp(duration);
        Destroy(gameObject);
    }
}