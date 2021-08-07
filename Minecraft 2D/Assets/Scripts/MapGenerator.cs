using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Tile dirtTile;
    [SerializeField] private Tilemap chunkTilemap;

    [SerializeField] private Transform gridContent;

    [Header("Settings")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;


    private const float GenerationTime = 0.05f;
    private readonly Dictionary<Vector2Int, Chunk> chunksDictionary = new Dictionary<Vector2Int, Chunk>();
    private readonly List<Chunk> chunksToGenerate = new List<Chunk>();

    private void Start()
    {
        CreateChunks(width, height);
    }

    private void CreateChunks(int width, int height)
    {
        Vector2 positionMutliplier = new Vector2(Chunk.Size, Chunk.Size);
        for (int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                var tilemapChunk = CreateChunk(chunkTilemap, new Vector2(x, y) * positionMutliplier);
                var chunk = new Chunk(tilemapChunk, x, y);
                chunksToGenerate.Add(chunk);
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

    private void GenerateChunk(Tilemap chunk)
    {
        for(int x = 0; x < Chunk.Size; ++x)
        {
            for (int y = 0; y < Chunk.Size; ++y)
            {
                chunk.SetTile(new Vector3Int(x, y, 0), dirtTile);
            }
        }
    }

    private IEnumerator GenerateMap()
    {
        while(chunksToGenerate.Count > 0)
        {
            GenerateChunk(chunksToGenerate[0].Tilemap);
            chunksDictionary.Add(chunksToGenerate[0].GetPosition(), chunksToGenerate[0]);
            chunksToGenerate.RemoveAt(0);
            yield return new WaitForSeconds(GenerationTime);
        }
    }
}
