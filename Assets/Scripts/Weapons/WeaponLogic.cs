using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] private GameObject weapon; // Na przyszłość, jakbyśmy wprowadzili więcej broni
    private GameObject ClosestEnemy;
    private float ClosestDistance;
    private int LastEnemiesCount;

    private void Start()
    {
        LastEnemiesCount = 0;
    }

    private void Update()
    {
        if (LastEnemiesCount != EntityBase.Enemies.Count)
        {
            FindTarget();
            LastEnemiesCount = EntityBase.Enemies.Count;
        }

        MoveWeapon();
    }

    private void FindTarget()
    {
        ClosestDistance = Mathf.Infinity;
        ClosestEnemy = null;
        foreach (EnemiesLogic enemy in EntityBase.Enemies)
        {
            if (!enemy) continue;
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < ClosestDistance)
            {
                ClosestDistance = distance;
                ClosestEnemy = enemy.gameObject;
            }
        }
    }

    private void MoveWeapon()
    {
        if (!ClosestEnemy) return; // Sposób sprawdzenia, czy obiekt istnieje w Unity. Cs-owe == null lub is null nie działa poprawnie. 
        Vector2 direction = ClosestEnemy!.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }

    public GameObject GetTarget()
    {
        return ClosestEnemy;
    }
}