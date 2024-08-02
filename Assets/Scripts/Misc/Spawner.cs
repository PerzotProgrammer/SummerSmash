using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float secondsInterval;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float minSpawnDistance;
    private PlayerLogic PlayerLogic;
    private Rigidbody2D PlayerRb;
    private bool CanSpawn;

    private void Start()
    {
        PlayerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
        PlayerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        CanSpawn = true;
    }

    private void Update()
    {
        if (CanSpawn && PlayerLogic.IsAlive()) StartCoroutine(nameof(Spawn));
    }

    private IEnumerator Spawn()
    {
        foreach (GameObject enemy in enemies) // TODO: System losowania przeciwników (jak będzie ich więcej)
        {
            Instantiate(enemy, RollPosition(), quaternion.identity);
        }

        CanSpawn = false;
        yield return new WaitForSeconds(secondsInterval);
        CanSpawn = true;
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