using DevKacper.Mechanic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevKacper.Extensions;

public class PlayerInventory : Inventory
{
    public PlayerInventory(int size) : base(size)
    {
        ;   
    }

    public override void AddItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        foreach (Slot slot in slots.Values)
        {
            if (slot.item == null)
            {
                ++slot.amount;
                slot.item = item;
                OnInventoryUpdated?.Invoke();
                return;
            }

            if (slot.item != null)
            {
                if (slot.item.Name == item.Name)
                {
                    if (slot.amount < BaseSlot.MaxSize)
                    {
                        ++slot.amount;
                        OnInventoryUpdated?.Invoke();
                        return;
                    }
                }
            }
        }
    }

    public override void AddItems(List<Item> items)
    {
        if (items.IsNullOrEmpty())
        {
            return;
        }

        foreach (Item item in items)
        {
            AddItem(item);
        }
    }

    public override void RemoveItem(Item item)
    {
        if(item == null)
        {
            return;
        }

        foreach (Slot slot in slots.Values)
        {
            if (slot.item != null && slot.item.Name == item.Name)
            {
                --slot.amount;

                if (slot.amount == 0)
                {
                    slot.item = null;
                }
                OnInventoryUpdated?.Invoke();
                return;
            }
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        if (item == null)
        {
            return;
        }

        int requiredAmount = amount;

        foreach (Slot slot in slots.Values)
        {
            if (slot.item != null && slot.item.ID == item.ID)
            {
                if(slot.amount >= requiredAmount)
                {
                    slot.amount -= requiredAmount;
                    requiredAmount = 0;
                }

                if(slot.amount == 0)
                {
                    slot.item = null;
                }
                OnInventoryUpdated?.Invoke();
                return;
            }
        }
    }

    public override void Swap(int firstItemID, int secondItemID)
    {
        Item firstItem = GetItem(firstItemID);
        Item secondItem = GetItem(secondItemID);

        slots[firstItemID].item = secondItem;
        slots[secondItemID].item = firstItem;

        OnInventoryUpdated?.Invoke();
    }

    public Slot[] GetSlotsArray()
    {
        Slot[] slots = new Slot[this.slots.Count];
        for(int i = 0; i < this.slots.Count; ++i)
        {
            slots[i] = this.slots[i];
        }
        return slots;
    }
}
