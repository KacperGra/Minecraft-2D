using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevKacper.Mechanic;

public class Player : MonoBehaviour
{
    private const int InventorySize = 25;

    public InventoryUI inventoryUI;
    public PlayerInventory inventory;

    public bool GetInput => !inventoryUI.IsInventoryOpen();

    private void Awake()
    {
        inventory = new PlayerInventory(InventorySize);
    }

    private void Start()
    {
        inventory.OnInventoryUpdated.AddListener(UpdateUI);
        inventoryUI.SetupSlots(InventorySize);
        UpdateUI();

        foreach (Slot slot in inventory.GetSlots().Values)
        {
            inventoryUI.GetSlot(slot.id).player = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryUI.ToggleMainInventory();
        }
    }

    private void UpdateUI()
    {
        foreach(Slot slot in inventory.GetSlots().Values)
        {
            if(slot.item == null)
            {
                inventoryUI.UpdateSlot(slot.id, null, 0);
                continue;
            }

            if(slot.item is BaseItem item)
            {
                inventoryUI.UpdateSlot(slot.id, item.sprite, slot.amount);
            }
        }
    }
}
