using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModification
{
    private TileType _type;
    private int _x;
    private int _y;

    public TileType Type => _type;
    public int X => _x;
    public int Y => _y;

    public MapModification(TileType type, int x, int y)
    {
        _type = type;
        _x = x;
        _y = y;
    }
}

public static class Structures
{
    public static List<MapModification> GetTree()
    {
        const int minTreeHeight = 3;
        const int maxTreeHeight = 6;

        List<MapModification> modList = new List<MapModification>();

        int height = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);

        // Logs
        modList.Add(new MapModification(TileType.TreeLogBottom, 0, 1));
        for (int i = 2; i < height; ++i)
        {
            modList.Add(new MapModification(TileType.TreeLogMid, 0, i));
        }

        modList.Add(new MapModification(TileType.TreeLog, 0, height));

        // Leaves
        modList.Add(new MapModification(TileType.TreeLeaves, -1, height));
        modList.Add(new MapModification(TileType.TreeLeaves, -2, height));
        modList.Add(new MapModification(TileType.TreeLeaves, 1, height));
        modList.Add(new MapModification(TileType.TreeLeaves, 2, height));

        modList.Add(new MapModification(TileType.TreeLeaves, 1, height + 1));
        modList.Add(new MapModification(TileType.TreeLeaves, 0, height + 1));
        modList.Add(new MapModification(TileType.TreeLeaves, -1, height + 1));

        modList.Add(new MapModification(TileType.TreeLeaves, 1, height + 2));
        modList.Add(new MapModification(TileType.TreeLeaves, 0, height + 2));
        modList.Add(new MapModification(TileType.TreeLeaves, -1, height + 2));

        modList.Add(new MapModification(TileType.TreeLeaves, 0, height + 3));

        return modList;
    }
}