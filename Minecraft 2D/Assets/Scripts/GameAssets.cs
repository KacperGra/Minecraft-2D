using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameAssets : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tile dirtTile;
    [SerializeField] private Tile dirtGrassTile;
    [SerializeField] private Tile stoneTile;
    [SerializeField] private Tile treeLogBottomTile;
    [SerializeField] private Tile treeLogMidTile;
    [SerializeField] private Tile treeLogTile;
    [SerializeField] private Tile treeLeavesTile;
    [SerializeField] private Tile grassTile;
    [SerializeField] private Tile plankTile;
    [SerializeField] private Tile coalTile;
    [SerializeField] private Tile ironTile;
    [SerializeField] private Tile goldTile;
    [SerializeField] private Tile diamondTile;

    [Header("UI")]
    [SerializeField] private SlotUI slotPrefab;
    [SerializeField] private CraftingSlotUI craftingSlotPrefab;

    public SlotUI SlotPrefab => slotPrefab;
    public CraftingSlotUI CraftingSlotPrefab => craftingSlotPrefab;

    public readonly Dictionary<ItemType, BaseItem> ItemsDictionary = new Dictionary<ItemType, BaseItem>();
    public readonly Dictionary<ItemType, CraftingRecipe> ItemRecipeDictionary = new Dictionary<ItemType, CraftingRecipe>();

    public static GameAssets Instance;

    private void Awake()
    {
        Instance = this;

        Resources.LoadAll("");

        foreach (BaseItem item in LoadFiles<BaseItem>())
        {
            ItemsDictionary.Add(item.ItemType, item);
        }

        foreach (CraftingRecipe recipe in LoadFiles<CraftingRecipe>())
        {
            ItemRecipeDictionary.Add(recipe.resoultItem.ItemType, recipe);
        }
    }

    public T[] LoadFiles<T>() where T : ScriptableObject
    {
        return Resources.FindObjectsOfTypeAll<T>();
    }

    public Tile GetTile(TileType tileType)
    {
        switch (tileType)
        {
            default:
                return null;

            case TileType.Dirt:
                return dirtTile;

            case TileType.DirtGrass:
                return dirtGrassTile;

            case TileType.Stone:
                return stoneTile;

            case TileType.TreeLogMid:
                return treeLogMidTile;

            case TileType.TreeLogBottom:
                return treeLogBottomTile;

            case TileType.TreeLog:
                return treeLogTile;

            case TileType.TreeLeaves:
                return treeLeavesTile;

            case TileType.Grass:
                return grassTile;

            case TileType.Plank:
                return plankTile;

            case TileType.CoalOre:
                return coalTile;

            case TileType.IronOre:
                return ironTile;

            case TileType.GoldOre:
                return goldTile;

            case TileType.DiamondOre:
                return diamondTile;
        }
    }

    public BaseItem GetItem(ItemType itemType)
    {
        return ItemsDictionary[itemType];
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

            case TileType.Plank:
                return GetItem(ItemType.Plank).Sprite;

            case TileType.CoalOre:
                return coalTile.sprite;

            case TileType.IronOre:
                return ironTile.sprite;

            case TileType.GoldOre:
                return goldTile.sprite;

            case TileType.DiamondOre:
                return diamondTile.sprite;
        }
    }

    public Sprite GetItemSprite(ItemType itemType)
    {
        TileType itemToTile = BaseItem.ItemToTile(itemType);
        if (itemToTile != TileType.Air)
        {
            return GetSprite(itemToTile);
        }

        switch (itemType)
        {
            case ItemType.Stick:
                return GetItem(itemType).Sprite;

            default:
                return null;
        }
    }
}