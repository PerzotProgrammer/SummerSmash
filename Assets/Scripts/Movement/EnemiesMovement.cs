using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceToSpotPlayer;
    private Rigidbody2D Rb;
    private GameObject Player;
    private PlayerDamage PlayerDamage;
    private float Distance;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        PlayerDamage = Player.GetComponent<PlayerDamage>();
    }

    void FixedUpdate()
    {
        if (PlayerDamage.IsAlive())
        {
            FollowPlayer();
        }
        else
        {
            Stay();
        }
    }

    void FollowPlayer()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (Distance < distanceToSpotPlayer)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.fixedDeltaTime);
        }
    }

    void Stay()
    {
        Rb.velocity = new Vector2(0, 0);
    }
}