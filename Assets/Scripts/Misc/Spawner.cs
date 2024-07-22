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
                float randX = Random.Range(-10, 10);
                float randY = Random.Range(-10, 10);
                Vector2 pos = new Vector2(randX + PlayerRb.position.x, randY + PlayerRb.position.y);
                Instantiate(enemy, pos, quaternion.identity);
            }
        }
    }
}