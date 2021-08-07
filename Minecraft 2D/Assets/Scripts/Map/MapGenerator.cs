using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap chunkTilemap;
    [SerializeField] private Transform gridContent;

    [Header("Settings")]
    [SerializeField] private int renderDistance = 3;

    private Position lastPlayerPosition;

    private const float GenerationTime = 0.05f;
    private const float DestroyingTime = 0.15f;


    private readonly Dictionary<Position, Chunk> chunksDictionary = new Dictionary<Position, Chunk>();
    private readonly List<Chunk> chunksToGenerate = new List<Chunk>();
    private readonly List<Chunk> activeChunks = new List<Chunk>();
    private readonly List<Chunk> chunksToDestroy = new List<Chunk>();

    private void Start()
    {
        Position playerPos = Chunk.GetObjectChunkPosition(player);
        lastPlayerPosition = playerPos;
        CreateChunks(renderDistance, playerPos);
    }

    private void Update()
    {
        Position playerPos = Chunk.GetObjectChunkPosition(player);
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

        for (int x = 0; x < Chunk.Size; ++x)
        {
            int maxHeight = MapSettings.MaxHeight;

            int perlinX = x + (int)chunk.Tilemap.transform.position.x + MapSettings.Seed;
            int perlinY = (int)chunk.Tilemap.transform.position.y + MapSettings.Seed;
            maxHeight -= (int)(Mathf.PerlinNoise(perlinX * .035f, perlinY * .035f) * 16);

            for (int y = 0; y < Chunk.Size; ++y)
            {
                int height = (int)chunk.Tilemap.transform.position.y + y;

                if(height > maxHeight)
                {
                    chunk.SetTile(TileType.Air, x, y);
                }
                else if (height == maxHeight)
                {
                    chunk.SetTile(TileType.DirtGrass, x, y);
                }
                else if (height > MapSettings.StoneLevel)
                {
                    chunk.SetTile(TileType.Dirt, x, y);
                }
                else if (height < MapSettings.StoneLevel)
                {
                    chunk.SetTile(TileType.Stone, x, y);
                }
            }
        }
        chunk.UpdateTexture();
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

            Debug.Log($"Generated chunk {chunksToGenerate[0].GetPosition()}");

            chunksToGenerate.RemoveAt(0);
            yield return new WaitForSeconds(GenerationTime);
        }

        yield return StartCoroutine(DestroyChunks());
    }

    private IEnumerator DestroyChunks()
    {
        while (chunksToDestroy.Count > 0)
        {
            chunksToDestroy[0].Tilemap.gameObject.SetActive(false);
            activeChunks.Remove(chunksToDestroy[0]);
            chunksToDestroy.RemoveAt(0);
            yield return new WaitForSeconds(GenerationTime);
        }
    }
}

public static class MapSettings
{
    public const int Seed = 4096;

    public const int MaxHeight = 49;
    public const int StoneLevel = 20;
}
