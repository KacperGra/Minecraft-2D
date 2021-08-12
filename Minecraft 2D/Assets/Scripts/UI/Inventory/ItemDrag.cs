using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : DevKacper.Mechanic.ItemDrag
{
    [HideInInspector] public Player player;
    [HideInInspector] public int slotID;


    public static ItemDrag CurrentDrag = null;

    protected override void Start()
    {
        base.Start();
        slotID = parent.GetComponent<SlotUI>().ID;
        player = parent.GetComponent<SlotUI>().player;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if(CurrentDrag == null)
        {
            base.OnBeginDrag(eventData);
            CurrentDrag = this;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(player.inventory.GetItem(slotID) == null || player.inventory.GetSlot(slotID).amount == 0)
        {
            CurrentDrag = null;
            OnEndDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        rectTransform.SetSiblingIndex(2);
        CurrentDrag = null;
    }
}
