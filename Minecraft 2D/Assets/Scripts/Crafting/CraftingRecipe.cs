using DevKacper.Mechanic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe
{
    public BaseItem resoultItem;
    public int amount;

    public CraftingComponent[] craftingComponents;

    public bool IsAbleToCraft(Slot[] itemSlots)
    {
        foreach(CraftingComponent component in craftingComponents)
        {
            int requiredAmount = component.amount;
            foreach(BaseSlot slot in itemSlots)
            {
                if(slot.item.ID == component.ID)
                {
                    requiredAmount -= slot.amount;

                    if (requiredAmount <= 0)
                    {
                        break;
                    }
                }
            }

            if(requiredAmount > 0)
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveComponents(PlayerInventory inventory)
    {
        foreach(CraftingComponent component in craftingComponents)
        {
            inventory.RemoveItem(component, component.amount);
        }
    }
}
