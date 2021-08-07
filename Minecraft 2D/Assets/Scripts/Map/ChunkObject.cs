using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkObject : MonoBehaviour
{
    private Chunk chunk;

    public void SetChunk(Chunk chunk)
    {
        this.chunk = chunk;
    }

    public TileType DestroyBlock(Vector3 position)
    {
        return chunk.DestroyBlock(position);
    }

    public void BuildTile(Vector3 position, TileType tileType)
    {
        chunk.SetTile(tileType, (int)position.x, (int)position.y);
    }
}
