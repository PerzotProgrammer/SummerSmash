using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int colisionDamage;
    protected Rigidbody2D Rb;

    protected void Stay()
    {
        Rb.velocity = new Vector2(0, 0);
    }

    public bool IsAlive()
    {
        if (hp <= 0) return false;
        return true;
    }

    public int GetHp()
    {
        return hp;
    }

    public virtual void InflictDamage(int damage)
    {
        hp -= damage;
        
    }

    public int GetColisionDamage()
    {
        return colisionDamage;
    }
}