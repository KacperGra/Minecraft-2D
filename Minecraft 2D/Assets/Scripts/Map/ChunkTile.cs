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

    public bool IsReplacable()
    {
        switch (tileType)
        {
            default:
                return false;

            case TileType.Air:
                return true;

            case TileType.Grass:
                return true;
        }
    }

    public bool IsDependOnBottomTile()
    {
        switch (tileType)
        {
            default:
                return false;

            case TileType.Grass:
                return true;
        }
    }
}