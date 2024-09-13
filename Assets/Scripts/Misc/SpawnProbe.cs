using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProbe : MonoBehaviour
{
    private bool IsInMapCollider;

    private void Start()
    {
        IsInMapCollider = false;
        Destroy(gameObject, Time.fixedDeltaTime * 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MapCollider")) IsInMapCollider = true;
    }

    public bool CheckIfIsInMapCollider()
    {
        return IsInMapCollider;
    }
}