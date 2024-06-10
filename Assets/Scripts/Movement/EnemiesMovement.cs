using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceToSpotPlayer;
    private GameObject Player;
    private float Distance;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        FollowPlayer();
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
}