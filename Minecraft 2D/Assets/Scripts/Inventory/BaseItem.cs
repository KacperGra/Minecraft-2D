using DevKacper.Mechanic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Base Item")]
public class BaseItem : Item
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemType itemType;
    [SerializeField] private TileType _tileType;

    [SerializeField] private bool _isBlock = true;
    [SerializeField] private bool _isStackable = true;

    public Sprite Sprite => sprite;

    public ItemType ItemType
    {
        get { return itemType; }
        set { SetItemType(value); }
    }

    public bool IsBlock => _isBlock;
    public bool IsStackable => _isStackable;

#if UNITY_EDITOR

    private void OnValidate()
    {
        id = (int)ItemType;
        itemName = itemType.ToString();
        name = itemName;
    }

#endif

    private void SetItemType(ItemType value)
    {
        itemType = value;
        id = (int)value;
        itemName = value.ToString();
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

            case ItemType.Plank:
                return TileType.Plank;

            case ItemType.CoalOre:
                return TileType.CoalOre;

            case ItemType.IronOre:
                return TileType.IronOre;

            case ItemType.GoldOre:
                return TileType.GoldOre;

            case ItemType.DiamondOre:
                return TileType.DiamondOre;
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

            case TileType.Plank:
                return ItemType.Plank;

            case TileType.CoalOre:
                return ItemType.CoalOre;

            case TileType.IronOre:
                return ItemType.IronOre;

            case TileType.GoldOre:
                return ItemType.GoldOre;

            case TileType.DiamondOre:
                return ItemType.DiamondOre;
        }
    }
}