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
    [SerializeField] private int featuresProbabilityMaxRange;
    [SerializeField] private TileBase groundAutoTile;
    [SerializeField] private TileBase waterAutoTile;
    [SerializeField] private TileBase[] groundFeatures;
    [SerializeField] private GameObject[] objectFeatures;
    private Tilemap GroundTilemap;
    private Tilemap GroundFeaturesTilemap;
    private GameObject Features;
    public static Dictionary<Vector3Int, TileType> TileTypes { get; private set; }

    private void Start()
    {
#if UNITY_EDITOR // Rozwiązanie tymczasowe, dopóki nie zaimplementuje się systemu chunków
        width = 100;
        height = 100;
#endif
        GroundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        GroundFeaturesTilemap = GameObject.Find("GroundFeatures").GetComponent<Tilemap>();
        Features = GameObject.Find("ObjectFeatures");
        TileTypes = new Dictionary<Vector3Int, TileType>();
        GenerateMap();
        GenerateFeatures();
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
                if (tileType == TileType.Ground && Random.Range(0, featuresProbabilityMaxRange) == 0)
                {
                    GroundFeaturesTilemap.SetTile(tilePosition, groundFeatures[Random.Range(0, groundFeatures.Length)]);
                }

                GroundTilemap.SetTile(tilePosition, tile);
                TileTypes.Add(tilePosition, tileType);
            }
        }
    }

    private void GenerateFeatures()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // TODO: Refactoring tego bo jest to nieoptymalne, na szybko napisane i nie działa do końca poprawnie
                if (TileTypes[new Vector3Int(x, y, 0)] == TileType.Ground &&
                    Random.Range(0, featuresProbabilityMaxRange * 3) == 0)
                {
                    Instantiate(objectFeatures[Random.Range(0, objectFeatures.Length)], Features.transform)
                        .transform.position = new Vector3Int(x, y, 0);
                }
            }
        }
    }

    private float[,] GeneratePerlinNoise()
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