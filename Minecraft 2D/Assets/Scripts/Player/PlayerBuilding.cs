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
            Chunk chunk = MapGenerator.GetChunk(mousePosition);
            ItemType itemType = chunk.DestroyBlock(mousePosition);
            if(itemType != ItemType.None)
            {
                BaseItem newItem = new BaseItem
                {
                    ItemType = itemType,
                    sprite = GameAssets.i.GetSprite(BaseItem.ItemToTile(itemType))
                };

                inventory.AddItem(newItem);

                Debug.Log($"Added {newItem.Name} to inventory.");
            }
        }
    }

    private void BuildInput(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(1))
        {
            Item item = inventory.GetItem(currentSlotIndex);
            if(item != null && item is BaseItem baseItem)
            {
                Chunk chunk = MapGenerator.GetChunk(mousePosition);

                if (chunk.BuildTile(mousePosition, BaseItem.ItemToTile(baseItem.ItemType)))
                {
                    inventory.RemoveItem(item);
                }
            }
        }
    }
}
