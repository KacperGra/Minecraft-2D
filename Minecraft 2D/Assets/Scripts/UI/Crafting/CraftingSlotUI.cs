using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlotUI : SlotUI, IPointerClickHandler
{
    public int recipeID;
    public PlayerCrafting playerCrafting;

    public void OnPointerClick(PointerEventData eventData)
    {
        playerCrafting.CraftItem(GameAssets.itemRecipeDictionary[(ItemType)recipeID]);
    }
}
