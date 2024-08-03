using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    protected float DespawnDistance;
    protected PlayerLogic PlayerLogic;

    private void Start()
    {
        DespawnDistance = 30;
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
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
}