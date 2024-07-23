using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int colisionDamage;
    protected int Hp;
    protected HealthBar HealthBar;
    protected Rigidbody2D Rb;

    protected void Stay()
    {
        Rb.velocity = new Vector2(0, 0);
    }

    public bool IsAlive()
    {
        if (Hp <= 0) return false;
        return true;
    }

    public int GetHp()
    {
        return Hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public void InflictDamage(int damage)
    {
        Hp -= damage;
        if (Hp < maxHp) HealthBar.ShowHealthBar();

        HealthBar.UpdateHealthBar();
    }

    public int GetColisionDamage()
    {
        return colisionDamage;
    }
}