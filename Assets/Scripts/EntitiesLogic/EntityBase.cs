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
    public static int KillCounter;
    public static List<EnemiesLogic> Enemies;

    protected void Stay()
    {
        Rb.velocity = new Vector2(0, 0);
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }

    public bool HasMaxHp()
    {
        return Hp == maxHp;
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
        HealthBar.UpdateHealthBar();
    }

    public int GetColisionDamage()
    {
        return colisionDamage;
    }
}