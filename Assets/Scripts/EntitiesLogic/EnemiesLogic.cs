using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLogic : EntityBase
{
    [SerializeField] private float distanceToSpotPlayer;
    private GameObject Player;
    private float Distance;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        HealthBar = GetComponentInChildren<HealthBar>();
        Hp = maxHp;
    }

    private void FixedUpdate()
    {
        if (Player is not null) FollowPlayer();
        else Stay();
        if (!IsAlive())
        {
            KillCounter += 1;
            Destroy(gameObject);
        }
        DespawnIfTooFar();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InflictDamage(other.gameObject.GetComponent<EntityBase>().GetColisionDamage());
        }
    }

    private void FollowPlayer()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (Distance < distanceToSpotPlayer)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.fixedDeltaTime);
        }
    }

    private void DespawnIfTooFar()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (Distance > distanceToSpotPlayer * 3) Destroy(gameObject);
    }
}