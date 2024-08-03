using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int colisionDamage;
    private bool IsOnDamageCooldown;
    protected int Hp;
    protected HealthBar HealthBar;
    protected Rigidbody2D Rb;
    public static int KillCounter;
    public static List<EnemiesLogic> Enemies;

    private IEnumerator DamageCooldown(int damage)
    {
        IsOnDamageCooldown = true;
        Hp -= damage;
        HealthBar.UpdateHealthBar();
        yield return new WaitForSeconds(0.5f); // Cooldown "taranowania"
        IsOnDamageCooldown = false;
    }

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

    public void InflictDamage(int damage, bool fromBullet = false)
    {
        if (!IsOnDamageCooldown || fromBullet) StartCoroutine(nameof(DamageCooldown), damage);
    }

    public int GetColisionDamage()
    {
        return colisionDamage;
    }
}