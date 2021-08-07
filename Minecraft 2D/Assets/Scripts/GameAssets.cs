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

    public static GameAssets i;
    private void Awake()
    {
        i = this;
    }
}
