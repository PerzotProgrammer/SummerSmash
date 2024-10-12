using System;
using System.Collections;
using System.Collections.Generic;
using Generation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private float noiseScale;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private TileBase groundAutoTile;
    [SerializeField] private TileBase waterAutoTile;
    private Tilemap Tilemap;
    public static Dictionary<Vector3Int, TileType> TileTypes;

    private void Start()
    {
        Tilemap = GetComponent<Tilemap>();
        TileTypes = new Dictionary<Vector3Int, TileType>();
        GenerateMap();
        gameObject.transform.position = new Vector3(-width / 2, -height / 2, 0);
    }

    private void GenerateMap()
    {
        float[,] noiseArray = GeneratePerlinNoise();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileType tileType;
                TileBase tile;

                if (noiseArray[x, y] > 0.5f)
                {
                    tile = groundAutoTile;
                    tileType = TileType.Ground;
                }
                else
                {
                    tile = waterAutoTile;
                    tileType = TileType.Water;
                }

                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Tilemap.SetTile(tilePosition, tile);
                TileTypes.Add(tilePosition, tileType);
            }
        }
    }

    public float[,] GeneratePerlinNoise()
    {
        float[,] noiseArray = new float[width, height];
        float seed = Random.Range(-10000, 10000);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sampleX = (x + seed) / noiseScale;
                float sampleY = (y + seed) / noiseScale;
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseArray[x, y] = perlinValue;
            }
        }

        return noiseArray;
    }
}