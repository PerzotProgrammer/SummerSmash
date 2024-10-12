using System;
using System.Collections;
using System.Collections.Generic;
using Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnProbe : MonoBehaviour
{
    private bool IsInMapCollider;
    private Tilemap Tilemap;

    private void Start()
    {
        Tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        // Tutaj na razie dlatego, że nie mam colliderów a undefined jest poza granicą mapy
        IsInMapCollider = GetCurrentTileType() == TileType.Undefined;
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

    protected TileType GetCurrentTileType()
    {
        Vector3Int probePosition = Tilemap.WorldToCell(transform.position);
        return MapGenerator.TileTypes.GetValueOrDefault(probePosition, TileType.Undefined);
    }
}