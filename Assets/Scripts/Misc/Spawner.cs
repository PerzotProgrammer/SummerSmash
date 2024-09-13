using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] pickups;
    [SerializeField] private GameObject spawnProbe;
    [SerializeField] private float enemySpawnCooldown;
    [SerializeField] private float pickupSpawnCooldown;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private int minSpawnCountPerCycle;
    [SerializeField] private int maxSpawnCountPerCycle;
    [SerializeField] private int maxSpawnedEnemiesCount;
    private PlayerLogic PlayerLogic;
    private Rigidbody2D PlayerRb;
    private bool IsOnEnemySpawnCooldown;
    private bool IsOnPickupSpawnCooldown;

    private void Start()
    {
        EntityBase.Enemies = new List<EnemiesLogic>();
        PickupBase.Pickups = new List<PickupBase>();
        EntityBase.KillCounter = 0;
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!IsOnEnemySpawnCooldown && PlayerLogic.IsAlive() && EntityBase.Enemies.Count < maxSpawnedEnemiesCount)
            StartCoroutine(nameof(SpawnEnemyCoroutine));
        if (!IsOnPickupSpawnCooldown && PlayerLogic.IsAlive())
            StartCoroutine(nameof(SpawnPickupCoroutine));
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        IsOnEnemySpawnCooldown = true;
        for (int i = 0; i < Random.Range(minSpawnCountPerCycle, maxSpawnCountPerCycle); i++)
        {
            Vector2 position = RollPosition();
            SpawnProbe sProbe = Instantiate(spawnProbe, position, Quaternion.identity).GetComponent<SpawnProbe>();
            yield return new WaitForSeconds(Time.fixedDeltaTime * 2); // Czas na wykrycie kolizji
            if (!sProbe.CheckIfIsInMapCollider())
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], position, quaternion.identity);
            }
            else i--;
        }

        yield return new WaitForSeconds(enemySpawnCooldown);
        IsOnEnemySpawnCooldown = false;
    }

    private IEnumerator SpawnPickupCoroutine()
    {
        IsOnPickupSpawnCooldown = true;
        for (int i = 0; i < Random.Range(minSpawnCountPerCycle, maxSpawnCountPerCycle); i++)
        {
            Vector2 position = RollPosition();
            SpawnProbe sProbe = Instantiate(spawnProbe, position, Quaternion.identity).GetComponent<SpawnProbe>();
            yield return new WaitForSeconds(Time.fixedDeltaTime * 2); // Czas na wykrycie kolizji
            if (!sProbe.CheckIfIsInMapCollider())
            {
                Instantiate(pickups[Random.Range(0, pickups.Length)], position, quaternion.identity);
            }
            else i--;
        }

        yield return new WaitForSeconds(pickupSpawnCooldown);
        IsOnPickupSpawnCooldown = false;
    }

    private Vector2 RollPosition()
    {
        Vector2 pos;
        float distance;

        do // Moje pierwsze w życiu poważne użycie do while
        {
            float randX = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            float randY = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos = new Vector2(randX + PlayerRb.position.x, randY + PlayerRb.position.y);
            distance = Vector2.Distance(pos, PlayerRb.transform.position);
        } while (distance < minSpawnDistance);

        return pos;
    }
}