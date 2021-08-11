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
        if(x >= 0 && y >= 0 && x < Size && y < Size)
        {
            tiles[x, y].tileType = tileType;
        }
    }

    public ItemType DestroyBlock(Vector3 position)
    {
        PositionToXY(position, out int x, out int y);

        if(tiles[x, y + 1].IsDependOnBottomTile())
        {
            tiles[x, y + 1].tileType = TileType.Air;
            UpdateTile(x, y + 1);
        }

        var tileType = tiles[x, y].tileType;
        tiles[x, y].tileType = TileType.Air;
        UpdateTile(x, y);

        return BaseItem.TileToItem(tileType);
    }

    public bool BuildTile(Vector3 position, TileType tileType)
    {
        PositionToXY(position, out int x, out int y);

        if (!tiles[x, y].IsReplacable())
        {
            return false;
        }

        SetTile(tileType, x, y);
        UpdateTile(x, y);
        return true;
    }

    public void UpdateTexture()
    {
        for (int x = 0; x < Size; ++x)
        {
            for (int y = 0; y < Size; ++y)
            {
                UpdateTile(x, y);
            }
        }
    }

    public void UpdateTile(int x, int y)
    {
        switch (tiles[x, y].tileType)
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
            case TileType.TreeLogMid:
                SetTile(x, y, GameAssets.i.treeLogMidTile);
                break;
            case TileType.TreeLogBottom:
                SetTile(x, y, GameAssets.i.treeLogBottomTile);
                break;
            case TileType.TreeLog:
                SetTile(x, y, GameAssets.i.treeLogTile);
                break;
            case TileType.TreeLeaves:
                SetTile(x, y, GameAssets.i.treeLeavesTile);
                break;
            case TileType.Grass:
                SetTile(x, y, GameAssets.i.grassTile);
                break;
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

    public TileType GetTile(Vector3 worldPosition)
    {
        PositionToXY(worldPosition, out int x, out int y);
        return GetTile(x, y);
    }

    private void PositionToXY(Vector3 position, out int x, out int y)
    {
        x = (int)position.x % Size;
        y = (int)position.y % Size;
        if (position.x <= 0)
        {
            x = Mathf.FloorToInt(Mathf.Abs(position.x + Size) % Size);
        }
    }

    public static Position GetWorldPositionToChunkPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / Size);
        int y = Mathf.FloorToInt(worldPosition.y / Size);
        return new Position(x, y);
    }
}
