using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    public const int Size = 25;

    private readonly Tilemap tilemap;
    private readonly int x;
    private readonly int y;

    private ChunkTile[,] tiles;

    public Tilemap Tilemap => tilemap;
    public Position GetPosition()
    {
        return new Position(x, y);
    }

    public Chunk(Tilemap tilemap, int x, int y)
    {
        this.tilemap = tilemap;
        this.x = x;
        this.y = y;

        SetupTiles();
    }

    private void SetupTiles()
    {
        tiles = new ChunkTile[Size, Size];
        for (int x = 0; x < Size; ++x)
        {
            for(int y = 0; y < Size; ++y)
            {
                tiles[x, y] = new ChunkTile(new Position(x, y));
            }
        }
    }

    public void SetTile(TileType tileType, int x, int y)
    {
        tiles[x, y].tileType = tileType; 
    }

    public void DestroyBlock(Vector3 position)
    {
        int x = Mathf.FloorToInt(Mathf.Abs(position.x) % Size);
        int y = Mathf.FloorToInt(Mathf.Abs(position.y) % Size);

        Debug.Log($"Destroyed block on X {x} Y {y}");
        tiles[x, y].tileType = TileType.Air;
        UpdateTexture();
    }

    public void UpdateTexture()
    {
        for (int x = 0; x < Size; ++x)
        {
            for (int y = 0; y < Size; ++y)
            {
                switch(tiles[x, y].tileType)
                {
                    case TileType.Air:
                        SetTile(x, y, null);
                        break;
                    case TileType.Dirt:
                        SetTile(x, y, GameAssets.i.dirtTile);
                        break;
                    case TileType.DirtGrass:
                        SetTile(x, y, GameAssets.i.dirtGrassTile);
                        break;
                    case TileType.Stone:
                        SetTile(x, y, GameAssets.i.stoneTile);
                        break;
                }
            }
        }
    }

    public void SetTile(int x, int y, Tile tile)
    {
        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }

    public TileType GetTile(int x, int y)
    {
        return tiles[x, y].tileType;
    }

    public static Position GetObjectChunkPosition(Transform transform)
    {
        int x = Mathf.FloorToInt(transform.position.x / Size);
        int y = Mathf.FloorToInt(transform.position.y / Size);
        return new Position(x, y);
    }
}
