using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTile
{
    private Position position;
    public Position Position => position;

    public TileType tileType;

    public ChunkTile(Position tilePosition)
    {
        position = tilePosition;
        this.tileType = TileType.Air;
    }
}
