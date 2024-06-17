using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D PlayerRb;
    private PlayerDamage PlayerDamage;
    private Vector2 MovementVector;
    
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerDamage = GetComponent<PlayerDamage>();
    }

    void FixedUpdate()
    {
        if (PlayerDamage.IsAlive())
        {
            Move();
        }
        else
        {
            Stay();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MovementVector = new Vector2(moveX, moveY).normalized;
        
        PlayerRb.velocity = new Vector2(MovementVector.x * speed, MovementVector.y * speed);
    }

    void Stay()
    {
        PlayerRb.velocity = new Vector2(0, 0);
    }

    public Vector2 MovementDirectionVector()
    {
        return MovementVector;
    }
}