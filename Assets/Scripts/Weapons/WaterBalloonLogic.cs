using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloonLogic : BulletBase
{
    [SerializeField] private int rotationAnglePerFixedUpdate;
    [SerializeField] private GameObject explosionPrefab;

    private void FixedUpdate()
    {
        CheckDistToPlayer();
        RotateAnim();
    }

    private void RotateAnim()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationAnglePerFixedUpdate);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies")) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}