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
    private float TimeElapsed;

    void Start()
    {
        TimeElapsed = 0;
    }

    void Update()
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
                Instantiate(enemy, new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)), quaternion.identity);
            }
        }
    }
}