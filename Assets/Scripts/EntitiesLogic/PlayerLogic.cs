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
        HealthBar = GetComponentInChildren<HealthBar>();
        Hp = maxHp;
        Enemies = new List<EnemiesLogic>();
    }

    private void FixedUpdate()
    {
        if (IsAlive()) Move();
        else Stay();
        if (!IsAlive()) gameObject.SetActive(false);
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MovementVector = new Vector2(moveX, moveY).normalized;
        Rb.velocity = new Vector2(MovementVector.x * speed, MovementVector.y * speed);
    }


    private void OnCollisionStay2D(Collision2D other)
    {
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