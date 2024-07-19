using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [CanBeNull] private GameObject ClosestEnemy;
    private float ClosestDistance;

    void FixedUpdate()
    {
        FindTarget();
        MoveWeapon();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    public GameObject GetTarget()
    {
        return ClosestEnemy;
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

    private void Shoot()
    {
        if (ClosestEnemy is not null)
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}