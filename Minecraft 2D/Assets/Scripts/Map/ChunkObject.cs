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
}
