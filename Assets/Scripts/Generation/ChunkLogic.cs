using Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkLogic : MonoBehaviour
{
    [SerializeField] private float renderDistance;
    [SerializeField] private int groundFeaturesProbabilityMaxRange;
    [SerializeField] private int objectFeaturesProbabilityMaxRange;
    private static GameObject Player;
    private TilemapRenderer TilemapRenderer;
    private TilemapRenderer GroundFeaturesTilemapRenderer;
    private Tilemap GroundTilemap;
    private Tilemap GroundFeaturesTilemap;
    private GameObject ObjectFeaturesParent;
    private float Distance;
    private bool IsRendered;
    private bool WasInitialized;
    private int XOffset;
    private int YOffset;

    private void Start()
    {
        Player = GameObject.Find("Player");
        TilemapRenderer = GetComponent<TilemapRenderer>();
        GroundFeaturesTilemapRenderer = transform.Find("GroundFeatures").GetComponent<TilemapRenderer>();
        GroundTilemap = GetComponent<Tilemap>();
        GroundFeaturesTilemap = transform.Find("GroundFeatures").GetComponent<Tilemap>();
        ObjectFeaturesParent = transform.Find("ObjectFeatures").gameObject;
    }

    private void FixedUpdate()
    {
        bool renderCheck = CheckDistanceForRendering();
        if (renderCheck && !IsRendered) RenderChunk();
        else if (!renderCheck && IsRendered) HideChunk();
    }

    private void RenderChunk()
    {
        if (WasInitialized)
        {
            TilemapRenderer.enabled = true;
            GroundFeaturesTilemapRenderer.enabled = true;
            ObjectFeaturesParent.SetActive(true);
            IsRendered = true;
            return;
        }

        InitializeChunk();
    }

    private void InitializeChunk()
    {
        for (int x = XOffset; x < MapGenerator.ChunkSize + XOffset; x++)
        {
            int localX = x - XOffset;
            for (int y = YOffset; y < MapGenerator.ChunkSize + YOffset; y++)
            {
                int localY = y - YOffset;
                TileType tileType;
                TileBase tile;

                if (MapGenerator.NoiseArray[x, y] > 0.5f)
                {
                    tile = MapGenerator.GroundAutoTile;
                    tileType = TileType.Ground;
                }
                else
                {
                    tile = MapGenerator.WaterAutoTile;
                    tileType = TileType.Water;
                }

                Vector3Int tilePosition = new Vector3Int(localX, localY, 0);

                if (tileType == TileType.Ground && Random.Range(0, objectFeaturesProbabilityMaxRange) == 0)
                {
                    Instantiate(MapGenerator.ObjectFeatures[Random.Range(0, MapGenerator.ObjectFeatures.Length)],
                        new Vector3(ObjectFeaturesParent.transform.position.x + localX,
                            ObjectFeaturesParent.transform.position.y + localY, 0), Quaternion.identity,
                        ObjectFeaturesParent.transform);
                }

                if (tileType == TileType.Ground && Random.Range(0, groundFeaturesProbabilityMaxRange) == 0)
                {
                    GroundFeaturesTilemap.SetTile(tilePosition,
                        MapGenerator.GroundFeatures[Random.Range(0, MapGenerator.GroundFeatures.Length)]);
                }

                GroundTilemap.SetTile(tilePosition, tile);
                MapGenerator.TileTypes.Add(new Vector3Int(x, y), tileType);
            }
        }

        WasInitialized = true;
        IsRendered = true;
    }

    private void HideChunk()
    {
        TilemapRenderer.enabled = false;
        GroundFeaturesTilemapRenderer.enabled = false;
        ObjectFeaturesParent.SetActive(false);
        IsRendered = false;
    }

    private bool CheckDistanceForRendering()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (Distance < renderDistance) return true;
        return false;
    }

    public void SetXYOffset(int x, int y)
    {
        XOffset = x;
        YOffset = y;
    }
}