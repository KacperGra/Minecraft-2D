using DevKacper.Mechanic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : Item
{
    public Sprite sprite;
    private ItemType itemType;

    public ItemType ItemType
    {
        get { return itemType; }
        set { SetItemType(value); }
    }

    private void SetItemType(ItemType value)
    {
        itemType = value;
        name = value.ToString();
    }

    public static TileType ItemToTile(ItemType itemType)
    {
        switch (itemType)
        {
            default:
                return TileType.Air;
            case ItemType.Dirt:
                return TileType.Dirt;
            case ItemType.DirtGrass:
                return TileType.DirtGrass;
            case ItemType.Stone:
                return TileType.Stone;
            case ItemType.TreeLog:
                return TileType.TreeLog;
            case ItemType.TreeLeaves:
                return TileType.TreeLeaves;
            case ItemType.Grass:
                return TileType.Grass;
        }
    }

    public static ItemType TileToItem(TileType tileType)
    {
        switch (tileType)
        {
            default:
                return ItemType.None;
            case TileType.Dirt:
                return ItemType.Dirt;
            case TileType.DirtGrass:
                return ItemType.DirtGrass;
            case TileType.Stone:
                return ItemType.Stone;
            case TileType.TreeLog:
            case TileType.TreeLogBottom:
            case TileType.TreeLogMid:
                return ItemType.TreeLog;
            case TileType.TreeLeaves:
                return ItemType.TreeLeaves;
            case TileType.Grass:
                return ItemType.Grass;
        }
    }
}
