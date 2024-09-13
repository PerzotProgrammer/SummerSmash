using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLogic : EntityBase
{
    [SerializeField] private float despawnDistance;
    private GameObject Player;
    private float Distance;


    private void Start()
    {
        InitBase();
        Enemies.Add(this);
        Player = GameObject.Find("Player");
        HealthBar = GetComponentInChildren<HealthBar>();
    }

    private void FixedUpdate()
    {
        if (Player)
        {
            FollowPlayer();
            DespawnIfTooFar();
        }
        else Stay();

        if (!IsAlive())
        {
            KillCounter += 1;
            Destroy(gameObject);
        }
    }


    protected override void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InflictDamage(other.gameObject.GetComponent<EntityBase>().GetColisionDamage());
            Stay(); // Naprawia problem z poruszaniem się wroga po jego dotknięciu
        }
    }

    private void FollowPlayer()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.fixedDeltaTime);
    }

    private void DespawnIfTooFar()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (Distance > despawnDistance) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Enemies.Remove(this);
    }
}