using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private readonly Vector2 BaseSlotSize = new Vector2(113, 113);
    private readonly Vector2 SelectedSlotSize = new Vector2(120, 120);


    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform mainContent;
    [SerializeField] private RectTransform mainInventory;

    private const int BottomContentSize = 5;

    private Dictionary<int, SlotUI> slots;

    public void SetupSlots(int inventorySize)
    {
        slots = new Dictionary<int, SlotUI>();
        for(int i = 0; i < inventorySize; ++i)
        {
            SlotUI newSlot;
            if(i < BottomContentSize)
            {
                newSlot = Instantiate(GameAssets.i.slotPrefab, content);
            }
            else
            {
                newSlot = Instantiate(GameAssets.i.slotPrefab, mainContent);
            }

            newSlot.Initalize(i);
            slots.Add(i, newSlot);
        }
    }

    public void UpdateSlot(int id, Sprite sprite, int amount)
    {
        slots[id].SetItem(sprite, amount);
    }

    public void SelectSlot(int id)
    {
        foreach(SlotUI slot in slots.Values)
        {
            if(slot.ID == id)
            {
                slot.RectTransform.sizeDelta = SelectedSlotSize;
            }
            else
            {
                slot.RectTransform.sizeDelta = BaseSlotSize;
            }
        }
    }

    public void ToggleMainInventory()
    {
        mainInventory.gameObject.SetActive(!mainInventory.gameObject.activeSelf);
    }
}
