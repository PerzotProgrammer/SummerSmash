using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    private float ClosestDistance;
    private GameObject ClosestEnemy;
    
    void FixedUpdate()
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

        // DEBUG: 
        Debug.DrawLine(transform.position, ClosestEnemy!.transform.position);
    }

    private void MoveWeapon()
    {
        Vector2 direction = ClosestEnemy.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }
}