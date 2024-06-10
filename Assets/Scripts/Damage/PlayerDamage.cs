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

    void Update()
    {
        // DEBUG: Linijka tymczasowa, dopóki nie ma UI
        Debug.Log(Hp);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            Hp -= 10;
        }

        if (!IsAlive())
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.enabled = false;
        }
    }

    public bool IsAlive()
    {
        if (Hp <= 0) return false;
        return true;
    }
}