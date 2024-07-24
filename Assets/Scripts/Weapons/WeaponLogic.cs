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


    private void Update()
    {
        FindTarget();
        MoveWeapon();
    }

    private void FindTarget()
    {
        ClosestDistance = Mathf.Infinity;
        ClosestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (GameObject enemy in enemies)
        {
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < ClosestDistance)
            {
                ClosestDistance = distance;
                ClosestEnemy = enemy;
            }
        }
    }

    private void MoveWeapon()
    {
        if (ClosestEnemy is null) return;
        Vector2 direction = ClosestEnemy!.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }

    public GameObject GetTarget()
    {
        return ClosestEnemy;
    }
}