using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    public const int Size = 25;

    private Tilemap tilemap;
    private int x;
    private int y;

    public Tilemap Tilemap => tilemap;
    public Vector2Int GetPosition()
    {
        return new Vector2Int(x, y);
    }

    public Chunk(Tilemap tilemap, int x, int y)
    {
        this.tilemap = tilemap;
        this.x = x;
        this.y = y;
    }
}
