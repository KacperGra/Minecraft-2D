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

    public void DestroyBlock(Vector3 position)
    {
        chunk.DestroyBlock(position);
    }
}
