using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLogic : EntityBase
{
    [SerializeField] private float distanceToSpotPlayer;
    private GameObject Player;
    private PlayerLogic PlayerLogic;
    private float Distance;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        PlayerLogic = Player.GetComponent<PlayerLogic>();
        HealthBar = GetComponentInChildren<HealthBar>();
        Hp = maxHp;
        // HealthBar.HideHealthBar(); // TODO: Coś tutaj kur*a nie działa
    }

    private void FixedUpdate()
    {
        if (PlayerLogic.IsAlive()) FollowPlayer();
        else Stay();
        if (!IsAlive()) Destroy(gameObject);
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
}