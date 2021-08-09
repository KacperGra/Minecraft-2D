using DevKacper.Utility;
using DevKacper.Mechanic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField] private Transform pointer;

    private PlayerInventory inventory;
    private InventoryUI inventoryUI;

    private int currentSlotIndex = 0;

    private void Start()
    {
        inventory = GetComponent<Player>().inventory;
        inventoryUI = GetComponent<Player>().inventoryUI;

        //inventoryUI.SelectSlot(currentSlotIndex);
    }

    private void Update()
    {
        pointer.transform.position = UtilityClass.GetGridMousePosition();

        DestroyInput(pointer.transform.position);
        BuildInput(pointer.transform.position);

        SlotInput();
    }

    private void SlotInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSlotIndex = 0;
            inventoryUI.SelectSlot(currentSlotIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlotIndex = 1;
            inventoryUI.SelectSlot(currentSlotIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlotIndex = 2;
            inventoryUI.SelectSlot(currentSlotIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentSlotIndex = 3;
            inventoryUI.SelectSlot(currentSlotIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentSlotIndex = 4;
            inventoryUI.SelectSlot(currentSlotIndex);
        }
    }

    private void DestroyInput(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var colliders = Physics2D.OverlapPointAll(mousePosition);
            foreach (Collider2D collider in colliders)
            {
                var chunk = collider.GetComponent<ChunkObject>();
                if (chunk != null)
                {
                    var ItemType = chunk.DestroyBlock(mousePosition);

                    BaseItem newItem = new BaseItem
                    {
                        ItemType = ItemType,
                        sprite = GameAssets.i.GetSprite(BaseItem.ItemToTile(ItemType))
                    };

                    Debug.Log($"Item {ItemType}");

                    inventory.AddItem(newItem);
                }
            }
        }
    }

    private void BuildInput(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(1))
        {
            var position = Chunk.GetObjectChunkPosition(pointer.transform);
            var chunk = MapGenerator.GetChunk(position);

            var item = inventory.GetItem(currentSlotIndex);
            if (item != null)
            {
                if (item is BaseItem baseItem)
                {
                    if(chunk.BuildTile(mousePosition, BaseItem.ItemToTile(baseItem.ItemType)))
                    {
                        inventory.RemoveItem(item);
                    }
                }
            }
        }
    }
}
