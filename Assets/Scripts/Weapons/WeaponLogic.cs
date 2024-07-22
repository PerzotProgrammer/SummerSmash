using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private GameObject ClosestEnemy;
    private float ClosestDistance;


    private void Update()
    {
        FindTarget();
        MoveWeapon();
        if (Input.GetButtonDown("Fire1") && ClosestEnemy is not null)
        {
            Shoot();
        }
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
        Instantiate(bullet, transform.position, transform.rotation);
    }

    public GameObject GetTarget()
    {
        return ClosestEnemy;
    }
}