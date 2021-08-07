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
    }
}
