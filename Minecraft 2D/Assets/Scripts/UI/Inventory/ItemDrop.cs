using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : DevKacper.Mechanic.ItemDrop, IPointerClickHandler
{
    protected override void Start()
    {
        base.Start();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop!");

        if (eventData == null || eventData.pointerDrag == null)
        {
            return;
        }

        var eventDrag = eventData.pointerDrag.GetComponent<ItemDrag>();

        if(itemDrag is ItemDrag minecraftItemDrag)
        {
            minecraftItemDrag.player.inventory.Swap(eventDrag.slotID, minecraftItemDrag.slotID);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null || eventData.pointerClick == null)
        {
            return;
        }

        if(ItemDrag.CurrentDrag == null)
        {
            return;
        }

        GetInventory().TransferItem(ItemDrag.CurrentDrag.slotID, GetID());
    }

    private PlayerInventory GetInventory()
    {
        if (itemDrag is ItemDrag minecraftItemDrag)
        {
            return minecraftItemDrag.player.inventory;
        }
        return null;
    }

    private int GetID()
    {
        if (itemDrag is ItemDrag minecraftItemDrag)
        {
            return minecraftItemDrag.slotID;
        }
        return -1;
    }
}
