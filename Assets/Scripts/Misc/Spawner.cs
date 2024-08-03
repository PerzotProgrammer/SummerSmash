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
    [SerializeField] private float enemySpawnCooldown;
    [SerializeField] private float pickupSpawnCooldown;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float minSpawnDistance;
    private PlayerLogic PlayerLogic;
    private Rigidbody2D PlayerRb;
    private bool EnemySpawnCooldown;
    private bool PickupSpawnCooldown;

    private void Start()
    {
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!EnemySpawnCooldown && PlayerLogic.IsAlive()) StartCoroutine(nameof(SpawnEnemyCoroutine));
        if (!PickupSpawnCooldown && PlayerLogic.IsAlive()) StartCoroutine(nameof(SpawnPickupCoroutine));
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        foreach (GameObject enemy in enemies) // TODO: System losowania przeciwników (jak będzie ich więcej)
        {
            Instantiate(enemy, RollPosition(), quaternion.identity);
        }

        EnemySpawnCooldown = true;
        yield return new WaitForSeconds(enemySpawnCooldown);
        EnemySpawnCooldown = false;
    }

    private IEnumerator SpawnPickupCoroutine()
    {
        foreach (GameObject pickup in pickups)
        {
            Instantiate(pickup, RollPosition(), quaternion.identity);
        }

        PickupSpawnCooldown = true;
        yield return new WaitForSeconds(pickupSpawnCooldown);
        PickupSpawnCooldown = false;
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