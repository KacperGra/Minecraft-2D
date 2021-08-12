using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    private const float GenerationTime = 0.05f;
    private const float DestroyingTime = 0.1f;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap chunkTilemap;
    [SerializeField] private Transform gridContent;

    [Header("Settings")]
    [SerializeField] private int renderDistance = 3;

    private Position lastPlayerPosition;

    private readonly Dictionary<Position, Chunk> chunksDictionary = new Dictionary<Position, Chunk>();
    private readonly List<Chunk> chunksToGenerate = new List<Chunk>();
    private readonly List<Chunk> activeChunks = new List<Chunk>();
    private readonly List<Chunk> chunksToDestroy = new List<Chunk>();

    public static MapGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Position playerPos = Chunk.GetWorldPositionToChunkPosition(player.position);
        lastPlayerPosition = playerPos;
        CreateChunks(renderDistance, playerPos);
    }

    private void Update()
    {
        Position playerPos = Chunk.GetWorldPositionToChunkPosition(player.position);
        if(playerPos != lastPlayerPosition)
        {
            CreateChunks(renderDistance, playerPos);
            lastPlayerPosition = playerPos;
        }
    }

    private void CreateChunks(int renderDistance, Position playerPos)
    {
        Vector2 positionMutliplier = new Vector2(Chunk.Size, Chunk.Size);
        
        for (int x = playerPos.x - renderDistance; x < playerPos.x + renderDistance; ++x)
        {
            for(int y = playerPos.y - 1; y < playerPos.y + 2; ++y)
            {
                Position position = new Position(x, y);
                if(chunksDictionary.ContainsKey(position))
                {
                    chunksDictionary[position].Tilemap.gameObject.SetActive(true);
                    activeChunks.Add(chunksDictionary[position]);
                    continue;
                }

                var tilemapChunk = CreateChunk(chunkTilemap, new Vector2(x, y) * positionMutliplier);
                var chunk = new Chunk(tilemapChunk, position.x, position.y);

                tilemapChunk.GetComponent<ChunkObject>().SetChunk(chunk);

                chunksToGenerate.Add(chunk);
                chunksDictionary.Add(chunk.GetPosition(), chunk);
            }
        }

        foreach (Chunk chunk in activeChunks)
        {
            if(chunksToDestroy.Contains(chunk))
            {
                if (IsChunkInRange(chunk))
                {
                    chunksToDestroy.Remove(chunk);
                }
                continue;
            }

            if (!IsChunkInRange(chunk))
            {
                chunksToDestroy.Add(chunk);
            }
        }

        StopCoroutine(GenerateMap());
        StartCoroutine(GenerateMap());
    }

    private Tilemap CreateChunk(Tilemap chunkTilemap, Vector3 position)
    {
        var chunk = Instantiate(chunkTilemap, gridContent);
        chunk.transform.position = position;
        return chunk;
    }

    private void GenerateChunk(Chunk chunk)
    {
        if(chunk.GetPosition().y > 1)
        {
            return;
        }

        SetupBlocks(chunk);
        AddOres(chunk);

        if(chunk.GetPosition().y == 1)
        {
            GenerateTrees(chunk);
            SmoothChunk(chunk);
        }
        
        chunk.UpdateTexture();
    }

    private void AddOres(Chunk chunk)
    {
        for (int x = 0; x < Chunk.Size; ++x)
        {
            for (int y = 0; y < Chunk.Size; ++y)
            {
                if (chunk.GetTile(x, y) == TileType.Stone)
                {
                    TileType oreToSpawn = TileType.Air;
                    float randomValue = Random.value;

                    if (randomValue <= MapSettings.ChanceForDiamond)
                    {
                        oreToSpawn = TileType.DiamondOre;
                    }
                    else if(randomValue <= MapSettings.ChanceForGold)
                    {
                        oreToSpawn = TileType.GoldOre;
                    }
                    else if (randomValue <= MapSettings.ChanceForIron)
                    {
                        oreToSpawn = TileType.IronOre;
                    }
                    else if (randomValue <= MapSettings.ChanceForCoal)
                    {
                        oreToSpawn = TileType.CoalOre;
                    }

                    if(oreToSpawn != TileType.Air)
                    {
                        chunk.SetTile(oreToSpawn, x, y);
                    }
                }
            }
        }
    }

    private void SetupBlocks(Chunk chunk)
    {
        for (int x = 0; x < Chunk.Size; ++x)
        {
            int maxHeight = MapSettings.MaxHeight;

            int perlinX = x + (int)chunk.Tilemap.transform.position.x + MapSettings.Seed;
            int perlinY = (int)chunk.Tilemap.transform.position.y + MapSettings.Seed;

            maxHeight -= (int)(Mathf.PerlinNoise(perlinX * MapSettings.PerlinMultiplier, perlinY * MapSettings.PerlinMultiplier) * MapSettings.MaxHeightDifference);

            int stoneLevel = MapSettings.MaxStoneLevel;
            stoneLevel -= (int)(Mathf.PerlinNoise(perlinX * MapSettings.PerlinMultiplier, perlinY * MapSettings.PerlinMultiplier) * MapSettings.MinStoneLevel);

            for (int y = 0; y < Chunk.Size; ++y)
            {
                int height = (int)chunk.Tilemap.transform.position.y + y;

                if (height > maxHeight)
                {
                    chunk.SetTile(TileType.Air, x, y);
                }
                else if (height == maxHeight)
                {
                    chunk.SetTile(TileType.DirtGrass, x, y);
                }
                else if (height > stoneLevel)
                {
                    chunk.SetTile(TileType.Dirt, x, y);
                }
                else if (height <= stoneLevel)
                {
                    chunk.SetTile(TileType.Stone, x, y);
                }
            }
        }
    }

    private void GenerateTrees(Chunk chunk)
    {
        int offsetX = 3;

        for (int x = offsetX; x < Chunk.Size - offsetX; ++x)
        {
            for (int y = 0; y < Chunk.Size; ++y)
            {
                if(chunk.GetTile(x, y) == TileType.DirtGrass)
                {
                    if (Random.value > MapSettings.ChanceForTree)
                    {
                        continue;
                    }

                    // Logs
                    chunk.SetTile(TileType.TreeLogBottom, x, y + 1);
                    chunk.SetTile(TileType.TreeLogMid, x, y + 2);
                    chunk.SetTile(TileType.TreeLog, x, y + 3);

                    // Leaves
                    chunk.SetTile(TileType.TreeLeaves, x - 1, y + 3);
                    chunk.SetTile(TileType.TreeLeaves, x - 2, y + 3);
                    chunk.SetTile(TileType.TreeLeaves, x + 1, y + 3);
                    chunk.SetTile(TileType.TreeLeaves, x + 2, y + 3);

                    chunk.SetTile(TileType.TreeLeaves, x + 1, y + 4);
                    chunk.SetTile(TileType.TreeLeaves, x, y + 4);
                    chunk.SetTile(TileType.TreeLeaves, x - 1, y + 4);

                    chunk.SetTile(TileType.TreeLeaves, x + 1, y + 5);
                    chunk.SetTile(TileType.TreeLeaves, x, y + 5);
                    chunk.SetTile(TileType.TreeLeaves, x - 1, y + 5);

                    chunk.SetTile(TileType.TreeLeaves, x, y + 6);
                }
            }
        }
    }

    private void SmoothChunk(Chunk chunk)
    {
        for (int x = 0; x < Chunk.Size; ++x)
        {
            for (int y = 0; y < Chunk.Size - 1; ++y)
            {
                if (Random.value > MapSettings.ChanceForGrass)
                {
                    continue;
                }
                if(chunk.GetTile(x, y + 1) != TileType.Air)
                {
                    continue;
                }

                if(chunk.GetTile(x, y) == TileType.DirtGrass)
                {
                    chunk.SetTile(TileType.Grass, x, y + 1);
                }
            }
        }
    }

    private bool IsChunkInRange(Chunk chunk)
    {
        int maxPosX = lastPlayerPosition.x + renderDistance;
        int minPosX = lastPlayerPosition.x - renderDistance;
        int maxPosY = lastPlayerPosition.y + 2;
        int minPosY = lastPlayerPosition.y - 1;

        if (chunk.GetPosition().x < minPosX || chunk.GetPosition().x > maxPosX || chunk.GetPosition().y < minPosY || chunk.GetPosition().y > maxPosY)
        {
            return false;
        }

        return true;
    }

    private IEnumerator GenerateMap()
    {
        while(chunksToGenerate.Count > 0)
        {
            GenerateChunk(chunksToGenerate[0]);
            activeChunks.Add(chunksToGenerate[0]);

            //Debug.Log($"Generated chunk {chunksToGenerate[0].GetPosition()}");

            chunksToGenerate.RemoveAt(0);
            yield return new WaitForSeconds(GenerationTime);
        }

        StopCoroutine(DestroyChunks());
        yield return StartCoroutine(DestroyChunks());
    }

    private IEnumerator DestroyChunks()
    {
        while (chunksToDestroy.Count > 0)
        {
            chunksToDestroy[0].Tilemap.gameObject.SetActive(false);
            activeChunks.Remove(chunksToDestroy[0]);
            chunksToDestroy.RemoveAt(0);
            yield return new WaitForSeconds(DestroyingTime);
        }
    }

    public static Chunk GetChunk(Position position)
    {
        return Instance.chunksDictionary[position];
    }

    public static Chunk GetChunk(Vector3 worldPosition)
    {
        Position position = Chunk.GetWorldPositionToChunkPosition(worldPosition);
        return GetChunk(position);
    }
}

public static class MapSettings
{
    public const int Seed = 4096;

    public const float PerlinMultiplier = 0.025f;

    public const int MaxHeight = 45;

    public const int MaxHeightDifference = 20;

    public const int MaxStoneLevel = 30;
    public const int MinStoneLevel = 15;

    public const float ChanceForTree = 0.035f;
    public const float ChanceForGrass = 0.25f;

    public const float ChanceForCoal = 0.2f;
    public const float ChanceForIron = 0.1f;
    public const float ChanceForGold = 0.035f;
    public const float ChanceForDiamond = 0.005f;
}
