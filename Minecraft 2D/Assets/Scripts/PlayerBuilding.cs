using DevKacper.Utility;
using DevKacper.Mechanic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField] private Transform pointer;

    private PlayerInventory inventory;

    private void Start()
    {
        inventory = GetComponent<Player>().inventory;
    }

    private void Update()
    {
        pointer.transform.position = UtilityClass.GetGridMousePosition();
        if(Input.GetMouseButtonDown(0))
        {
            var colliders = Physics2D.OverlapPointAll(pointer.transform.position);
            foreach(Collider2D collider in colliders)
            {
                var chunk = collider.GetComponent<ChunkObject>();
                if(chunk != null)
                {
                    var tileType = chunk.DestroyBlock(pointer.transform.position);
                    BaseItem newItem = new BaseItem
                    {
                        tileType = tileType
                    };

                    Debug.Log($"Item {tileType}");

                    inventory.AddItem(newItem);
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            var position = Chunk.GetObjectChunkPosition(pointer.transform);
            var chunk = MapGenerator.GetChunk(position);

            int x = (int)pointer.position.x % Chunk.Size;
            int y = (int)pointer.position.y % Chunk.Size;
            if (pointer.position.x <= 0)
            {
                x = Mathf.FloorToInt(Mathf.Abs(pointer.position.x + (Chunk.Size * Mathf.Abs(position.x) + 1)) % Chunk.Size);
            }

            chunk.SetTile(TileType.Dirt, x, y);
            chunk.UpdateTile(x, y);
        }

    }


}
