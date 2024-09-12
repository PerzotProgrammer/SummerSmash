using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    protected float DespawnDistance;
    protected PlayerLogic PlayerLogic;
    public static List<PickupBase> Pickups;


    private void Start()
    {
        DespawnDistance = 30;
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        Pickups.Add(this);
    }

    private void Update()
    {
        DespawnIfTooFar();
    }

    protected void DespawnIfTooFar()
    {
        float distance = Vector2.Distance(transform.position, PlayerLogic.transform.position);
        if (distance > DespawnDistance) Destroy(gameObject);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void Pickup(EntityBase entityBase);

    private void OnDestroy()
    {
        Pickups.Remove(this);
    }
}