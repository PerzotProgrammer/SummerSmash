using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private int Hp;

    void Start()
    {
        Hp = 100;
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
            Hp -= 10;
        }
    }

    public bool IsAlive()
    {
        if (Hp <= 0) return false;
        return true;
    }

    public float GetHp()
    {
        return Hp;
    }
}