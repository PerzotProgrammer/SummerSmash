using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    protected float DespawnDistance;
    protected PlayerLogic PlayerLogic;
    public static List<PickupBase> Pickups { get; private set; }


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

    protected virtual void Pickup(EntityBase entityBase)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Pickups.Remove(this);
    }

    public static void InitPickupList()
    {
        Pickups = new List<PickupBase>();
    }
}