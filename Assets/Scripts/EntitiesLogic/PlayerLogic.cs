using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : EntityBase
{
    private Vector2 MovementVector;
    private bool IsOnSpeedUp;

    private void Start()
    {
        InitBase();
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

    private IEnumerator SpeedUpCoroutine(float duration)
    {
        float prevSpeed = speed;
        IsOnSpeedUp = true;
        speed *= 1.5f;
        yield return new WaitForSeconds(duration);
        speed = prevSpeed;
        IsOnSpeedUp = false;
    }

    public void SpeedUp(float duration)
    { 
        StartCoroutine(nameof(SpeedUpCoroutine), duration);
    }

    public void HealHp(int healedHp)
    {
        Hp += healedHp;
        if (Hp > maxHp) Hp = maxHp;
        HealthBar.UpdateHealthBar();
    }

    public bool IsOnSpeedBoost()
    {
        return IsOnSpeedUp;
    }

    public Vector2 GetMovementVector()
    {
        return MovementVector;
    }
}