using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Generation;

public class PlayerLogic : EntityBase
{
    protected override void Start()
    {
        base.Start();
        KillCounter = 0;
    }

    private void FixedUpdate()
    {
        if (IsAlive()) Move();
        else Stay();
        if (!IsAlive())
        {
            if (!SceneManager.GetSceneByName("GameOverMenu").isLoaded)
            {
                HealthBar.gameObject.SetActive(false);
                gameObject.SetActive(false);
                SceneManager.UnloadSceneAsync("UI");
                SceneManager.LoadSceneAsync("GameOverMenu", LoadSceneMode.Additive);
            }
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MovementVector = new Vector2(moveX, moveY).normalized;
        Rb.linearVelocity = new Vector2(MovementVector.x * speed, MovementVector.y * speed);
        if (GetCurrentTileType() == TileType.Water) Rb.linearVelocity *= 0.8f;
    }

    public void SetHealthBar(HealthBar healthBar)
    {
        HealthBar = healthBar;
    }

    public void AddMaxHp(int amount)
    {
        MaxHp += amount;
        HealthBar.UpdateHealthBar();
    }

    protected override void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            InflictDamage(other.gameObject.GetComponent<EntityBase>().CollisionDamage);
        }
    }
}