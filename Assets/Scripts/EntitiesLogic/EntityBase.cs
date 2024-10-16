using System;
using System.Collections;
using System.Collections.Generic;
using Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class EntityBase : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int collisionDamage;
    private bool IsOnDamageCooldown;
    protected bool IsOnSpeedUp;
    protected int Hp;
    protected HealthBar HealthBar;
    protected Rigidbody2D Rb;
    protected Vector2 MovementVector;
    public static int KillCounter;
    public static List<EnemiesLogic> Enemies;
    private Tilemap Tilemap;

    private IEnumerator DamageCooldownCoroutine(int damage)
    {
        IsOnDamageCooldown = true;
        Hp -= damage;
        HealthBar.UpdateHealthBar();
        yield return new WaitForSeconds(0.5f); // Cooldown "taranowania"
        IsOnDamageCooldown = false;
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

    protected void InitBase()
    {
        Rb = GetComponent<Rigidbody2D>();
        Hp = maxHp;
        Tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
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
        if (!IsOnDamageCooldown || fromBullet) StartCoroutine(nameof(DamageCooldownCoroutine), damage);
    }

    public int GetColisionDamage()
    {
        return collisionDamage;
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

    protected TileType GetCurrentTileType()
    {
        Vector3Int playerPosition = Tilemap.WorldToCell(transform.position);
        return MapGenerator.TileTypes.GetValueOrDefault(playerPosition, TileType.Undefined);
    }

    protected abstract void OnCollisionStay2D(Collision2D other);
}