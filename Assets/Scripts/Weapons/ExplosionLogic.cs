using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float despawnCooldown;

    private void Start()
    {
        Destroy(gameObject, despawnCooldown);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EntityBase entityBase = other.gameObject.GetComponent<EntityBase>();
        if (!entityBase) return;

        if (entityBase.CompareTag("Player"))
        {
            entityBase.InflictDamage(damage / 2, true);
            return;
        }

        entityBase.InflictDamage(damage, true);
    }
}