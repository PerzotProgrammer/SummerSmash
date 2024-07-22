using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : EntityBase
{
    private Vector2 MovementVector;


    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        hp = 100;
    }

    private void FixedUpdate()
    {
        if (IsAlive()) Move();
        else Stay();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MovementVector = new Vector2(moveX, moveY).normalized;

        Rb.velocity = new Vector2(MovementVector.x * speed, MovementVector.y * speed);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsAlive())
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            return;
        }

        if (other.gameObject.CompareTag("Enemies"))
        {
            InflictDamage(other.gameObject.GetComponent<EntityBase>().GetColisionDamage());
        }
    }

    public Vector2 GetMovementVector()
    {
        return MovementVector;
    }
}