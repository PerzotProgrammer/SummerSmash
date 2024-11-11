using System;
using System.Collections;
using System.Collections.Generic;
using Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class EntityBase : MonoBehaviour
{
    [SerializeField] protected int startingHp;
    [SerializeField] protected int startingCollisionDamage;
    [SerializeField] protected float speed;
    [SerializeField] protected AudioClip hitSound;
    private bool IsOnDamageCooldown;
    private Tilemap Tilemap;
    protected HealthBar HealthBar;
    protected Rigidbody2D Rb;
    protected AudioSource AudioSource;
    public static int KillCounter { get; protected set; }
    public static List<EnemiesLogic> Enemies { get; protected set; }
    public int Hp { get; protected set; }
    public int MaxHp { get; protected set; }
    public int CollisionDamage { get; protected set; }
    public bool IsOnSpeedUp { get; protected set; }
    public Vector2 MovementVector { get; protected set; }

    private IEnumerator DamageCooldownCoroutine(int damage)
    {
        IsOnDamageCooldown = true;
        Hp -= damage;
        HealthBar.UpdateHealthBar();
        AudioSource.clip = hitSound;
        AudioSource.Play();
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

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
        CollisionDamage = startingCollisionDamage;
        MaxHp = startingHp;
        Hp = MaxHp;
        Tilemap = GameObject.Find("Grid").GetComponent<Tilemap>();
    }

    protected void Stay()
    {
        Rb.linearVelocity = new Vector2(0, 0);
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }

    public bool HasMaxHp()
    {
        return Hp == MaxHp;
    }

    public void InflictDamage(int damage, bool fromBullet = false)
    {
        if (!IsOnDamageCooldown || fromBullet) StartCoroutine(nameof(DamageCooldownCoroutine), damage);
    }

    public void SpeedUp(float duration)
    {
        StartCoroutine(nameof(SpeedUpCoroutine), duration);
    }

    public void HealHp(int healedHp)
    {
        Hp += healedHp;
        if (Hp > MaxHp) Hp = MaxHp;
        HealthBar.UpdateHealthBar();
    }


    protected TileType GetCurrentTileType()
    {
        Vector3Int playerPosition = Tilemap.WorldToCell(transform.position);
        return MapGenerator.TileTypes.GetValueOrDefault(playerPosition, TileType.Undefined);
    }

    protected abstract void OnCollisionStay2D(Collision2D other);

    public static void InitEnemiesList()
    {
        Enemies = new List<EnemiesLogic>();
    }
}