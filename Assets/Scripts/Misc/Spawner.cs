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
    private Rigidbody2D PlayerRb;
    private float TimeElapsed;

    private void Start()
    {
        PlayerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        TimeElapsed = 0;
    }

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed >= secondsInterval)
        {
            TimeElapsed = 0;
            foreach (GameObject enemy in enemies)
            {
                Instantiate(enemy, RollPosition(), quaternion.identity);
            }
        }
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