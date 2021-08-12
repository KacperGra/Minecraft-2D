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

    public static readonly Dictionary<ItemType, BaseItem> itemsDictionary = new Dictionary<ItemType, BaseItem>();
    public static readonly Dictionary<ItemType, CraftingRecipe> itemRecipeDictionary = new Dictionary<ItemType, CraftingRecipe>();

    [Header("UI")]
    public SlotUI slotPrefab;
    public CraftingSlotUI craftingSlotPrefab;

    public static GameAssets i;
    private void Awake()
    {
        i = this;

        Resources.LoadAll("");

        foreach (BaseItem item in LoadFiles<BaseItem>())
        {
            itemsDictionary.Add(item.ItemType, item);
        }

        foreach (CraftingRecipe recipe in LoadFiles<CraftingRecipe>())
        {
            itemRecipeDictionary.Add(recipe.resoultItem.ItemType, recipe);
        }
    }

    private void Start()
    {

    }

    public T[] LoadFiles<T>() where T : ScriptableObject
    {
        return Resources.FindObjectsOfTypeAll<T>();
    }

    public BaseItem GetItem(ItemType itemType)
    {
        return itemsDictionary[itemType];
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
                return GetItem(ItemType.Plank).sprite;
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
                return GetItem(itemType).sprite;
            default:
                return null;
        }
    }
}
