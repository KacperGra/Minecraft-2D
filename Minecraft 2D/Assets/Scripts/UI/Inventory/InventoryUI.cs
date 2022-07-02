using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    private readonly Vector2 BaseSlotSize = new Vector2(113, 113);
    private readonly Vector2 SelectedSlotSize = new Vector2(120, 120);

    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform mainContent;
    [SerializeField] private RectTransform mainInventory;

    [SerializeField] private int _bottomContentSize = 5;

    private bool _isOpen;
    private Dictionary<int, SlotUI> slots;

    public void SetupSlots(int inventorySize)
    {
        slots = new Dictionary<int, SlotUI>();
        for (int i = 0; i < inventorySize; ++i)
        {
            SlotUI newSlot;
            if (i < _bottomContentSize)
            {
                newSlot = Instantiate(GameAssets.Instance.SlotPrefab, content);
            }
            else
            {
                newSlot = Instantiate(GameAssets.Instance.SlotPrefab, mainContent);
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
        foreach (SlotUI slot in slots.Values)
        {
            if (slot.ID == id)
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
        if (DOTween.IsTweening(mainInventory, true))
        {
            mainInventory.DOKill();
        }

        _isOpen = !_isOpen;
        if (!_isOpen)
        {
            mainInventory.gameObject.SetActive(true);

            Sequence fadeInSequence = DOTween.Sequence(mainInventory);
            fadeInSequence.SetEase(Ease.OutBack);
            fadeInSequence.Append(mainInventory.DOScale(new Vector3(1f, 1f, 1f), 0.75f));

            fadeInSequence.Play();
        }
        else
        {
            Sequence fadeOutSequence = DOTween.Sequence(mainInventory);
            fadeOutSequence.SetEase(Ease.OutBack);
            fadeOutSequence.Append(mainInventory.DOScale(new Vector3(0f, 0f, 0f), 0.5f));

            fadeOutSequence.Play().OnComplete(() =>
            {
                mainInventory.gameObject.SetActive(false);
            });
        }
    }

    public bool IsInventoryOpen()
    {
        return mainInventory.gameObject.activeSelf;
    }

    public SlotUI GetSlot(int id)
    {
        return slots[id];
    }
}