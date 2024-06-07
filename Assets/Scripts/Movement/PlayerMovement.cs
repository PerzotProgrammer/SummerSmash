using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D PlayerRb;

    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movementVector = new Vector2(moveX, moveY).normalized;

        PlayerRb.velocity = new Vector2(movementVector.x * speed, movementVector.y * speed);
    }
}