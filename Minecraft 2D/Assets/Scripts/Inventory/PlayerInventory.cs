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
        AddItem(item, 1);
    }

    public void AddItem(Item item, int amount = 1)
    {
        if (item == null)
        {
            return;
        }

        int slotID = GetSlotIDWithItem(item, amount);
        if(slotID != -1)
        {
            AddItem(amount, slotID);
            return;
        }

        foreach (Slot slot in slots.Values)
        {
            if (slot.item == null)
            {
                slot.amount += amount;
                slot.item = item;
                OnInventoryUpdated?.Invoke();
                return;
            }

            if(slot.item is BaseItem baseItem)
            {
                if(BaseItem.IsStackable(baseItem.ItemType))
                {
                    continue;
                }
            }

            if (slot.item != null)
            {
                if (slot.item.Name == item.Name)
                {
                    if (slot.amount + amount < BaseSlot.MaxSize)
                    {
                        slot.amount += amount;
                        OnInventoryUpdated?.Invoke();
                        return;
                    }
                }
            }
        }
    }

    public void AddItem(int amount, int slotID)
    {
        slots[slotID].amount += amount;
        OnInventoryUpdated?.Invoke();
    }

    public void AddOneItem(Item item, int id)
    {
        slots[id].item = item;
        ++slots[id].amount;
        OnInventoryUpdated?.Invoke();
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

    public void RemoveOneItem(int id)
    {
        if(slots[id].item != null)
        {
            --slots[id].amount;
            OnInventoryUpdated?.Invoke();
        }
    }

    public void TransferItem(int fromID, int toID)
    {
        if (GetSlot(fromID).item is BaseItem baseItem)
        {
            if (!BaseItem.IsStackable(baseItem.ItemType))
            {
                return;
            }
        }

        if (GetItem(toID) != null)
        {
            if (GetItem(fromID).Name == GetItem(toID).Name)
            {
                AddOneItem(GetItem(fromID), toID);
                RemoveOneItem(fromID);
            }
        }
        else
        {
            AddOneItem(GetItem(fromID), toID);
            RemoveOneItem(fromID);
        }
    }

    public bool IsItemInInventory(Item item, int amount = 1)
    {
        int requiredAmount = amount;
        foreach(Slot slot in slots.Values)
        {
            if(slot.item == null)
            {
                continue;
            }

            if(slot.item.ID == item.ID)
            {
                requiredAmount -= slot.amount;

                if(requiredAmount <= 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int GetSlotIDWithItem(Item item, int amount)
    {
        if(IsItemInInventory(item, 1))
        {
            foreach(Slot slot in slots.Values)
            {
                if (slot.item == null)
                {
                    continue;
                }

                if(slot.item.ID == item.ID)
                {
                    if(slot.amount + amount > BaseSlot.MaxSize)
                    {
                        continue;
                    }

                    return slot.id;
                }
            }
        }

        return -1;
    }

    public override void Swap(int firstItemID, int secondItemID)
    {
        Item firstItem = GetSlot(firstItemID).item;
        int firstAmount = GetSlot(firstItemID).amount;

        Item secondItem = GetSlot(secondItemID).item;
        int secondAmount = GetSlot(secondItemID).amount;

        if(TryMergeItems(firstItem, secondItem, firstItemID, secondItemID, firstAmount))
        {
            return;
        }

        slots[firstItemID].item = secondItem;
        slots[firstItemID].amount = secondAmount;

        slots[secondItemID].item = firstItem;
        slots[secondItemID].amount = firstAmount;

        OnInventoryUpdated?.Invoke();
    }

    private bool TryMergeItems(Item firstItem, Item secondItem, int firstItemID, int secondItemID, int firstAmount)
    {
        if (firstItem != null && secondItem != null)
        {
            if (firstItem.ID == secondItem.ID)
            {
                slots[firstItemID].Clear();
                slots[secondItemID].amount += firstAmount;

                if (slots[secondItemID].amount > BaseSlot.MaxSize)
                {
                    int amount = slots[secondItemID].amount - BaseSlot.MaxSize;
                    slots[secondItemID].amount = BaseSlot.MaxSize;

                    AddItem(slots[secondItemID].item, amount);
                }

                OnInventoryUpdated?.Invoke();
                return true;
            }
        }

        return false;
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

    public override void SetItem(Item item, int id)
    {
        if(item == null)
        {
            return;
        }

        slots[id].item = item;
        slots[id].amount = 1;
        OnInventoryUpdated?.Invoke();
    }
}
