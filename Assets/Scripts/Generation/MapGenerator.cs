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
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private int chunkCount;
    [SerializeField] private int chunkSize;
    [SerializeField] private TileBase groundAutoTile;
    [SerializeField] private TileBase waterAutoTile;
    [SerializeField] private TileBase[] groundFeatures;
    [SerializeField] private GameObject[] objectFeatures;
    public static TileBase GroundAutoTile { get; private set; }
    public static TileBase WaterAutoTile { get; private set; }
    public static TileBase[] GroundFeatures { get; private set; }
    public static GameObject[] ObjectFeatures { get; private set; }
    public static int ChunkCount { get; private set; }
    public static int ChunkSize { get; private set; }
    public static Dictionary<Vector3Int, TileType> TileTypes { get; private set; }
    public static float[,] NoiseArray { get; private set; }

    private void Start()
    {
        GroundAutoTile = groundAutoTile;
        WaterAutoTile = waterAutoTile;
        GroundFeatures = groundFeatures;
        ObjectFeatures = objectFeatures;
        ChunkCount = chunkCount;
        ChunkSize = chunkSize;
        TileTypes = new Dictionary<Vector3Int, TileType>();
        NoiseArray = GeneratePerlinNoise();
        GenerateMap();
        gameObject.transform.position = new Vector3(-chunkCount * chunkSize / 2, -chunkCount * chunkSize / 2, 0);
    }

    private void GenerateMap()
    {
        for (int i = 0; i < chunkCount; i++)
        {
            for (int j = 0; j < chunkCount; j++)
            {
                GameObject chunk = Instantiate(chunkPrefab, transform);
                chunk.transform.position = new Vector3(i * chunkSize, j * chunkSize, 0);
                chunk.GetComponent<ChunkLogic>().SetXYOffset(i * chunkSize, j * chunkSize);
            }
        }
    }


    private float[,] GeneratePerlinNoise()
    {
        float[,] noiseArray = new float[chunkCount * chunkSize, chunkCount * chunkSize];
        float seed = Random.Range(-10000, 10000);

        for (int x = 0; x < noiseArray.GetLength(0); x++)
        {
            for (int y = 0; y < noiseArray.GetLength(1); y++)
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