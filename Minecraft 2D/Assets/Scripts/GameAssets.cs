using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameAssets : MonoBehaviour
{
    [Header("Tilemap")]
    public Tile dirtTile;
    public Tile dirtGrassTile;
    public Tile stoneTile;
    public Tile treeLogBottomTile;
    public Tile treeLogMidTile;
    public Tile treeLogTile;
    public Tile treeLeavesTile;
    public Tile grassTile;

    [Header("UI")]
    public SlotUI slotPrefab;

    public static GameAssets i;
    private void Awake()
    {
        i = this;
    }

    public Sprite GetSprite(TileType tileType)
    {
        switch (tileType)
        {
            default:
            case TileType.Air:
                return null;
            case TileType.Dirt:
                return dirtTile.sprite;
            case TileType.DirtGrass:
                return dirtGrassTile.sprite;
            case TileType.Stone:
                return stoneTile.sprite;
            case TileType.TreeLogBottom:
                return treeLogBottomTile.sprite;
            case TileType.TreeLogMid:
                return treeLogMidTile.sprite;
            case TileType.TreeLog:
                return treeLogTile.sprite;
            case TileType.TreeLeaves:
                return treeLeavesTile.sprite;
            case TileType.Grass:
                return grassTile.sprite;
        }
    }
}
