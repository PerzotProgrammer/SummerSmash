using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    // TODO: To jest rozwiązanie tymczasowe. Przerobić potem BulletLogic.cs
    [SerializeField] private GameObject explosion;
    [SerializeField] private float despawnCooldown;

    private void OnDestroy()
    {
        GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionInstance, despawnCooldown);
    }
}