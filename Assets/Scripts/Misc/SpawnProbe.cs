using System;
using System.Collections;
using System.Collections.Generic;
using Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnProbe : MonoBehaviour
{
    private Tilemap Tilemap;
    public bool IsInMapCollider { get; private set; }

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

    protected TileType GetCurrentTileType()
    {
        Vector3Int probePosition = Tilemap.WorldToCell(transform.position);
        return MapGenerator.TileTypes.GetValueOrDefault(probePosition, TileType.Undefined);
    }
}