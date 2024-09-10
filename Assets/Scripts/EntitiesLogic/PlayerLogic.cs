using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : EntityBase
{
    private void Start()
    {
        InitBase();
        KillCounter = 0;
        Enemies = new List<EnemiesLogic>();
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
        Rb.velocity = new Vector2(MovementVector.x * speed, MovementVector.y * speed);
    }

    public void SetHealthBar(HealthBar healthBar)
    {
        HealthBar = healthBar;
    }

    protected override void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            InflictDamage(other.gameObject.GetComponent<EntityBase>().GetColisionDamage());
        }
    }
}