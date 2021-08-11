using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    private PlayerInventory inventory;

    private void Start()
    {
        inventory = GetComponent<Player>().inventory;
    }

    public void CraftItem(CraftingRecipe recipe)
    {
        if(recipe.IsAbleToCraft(inventory.GetSlotsArray()))
        {
            recipe.RemoveComponents(inventory);
            inventory.AddItem(recipe.resoultItem);
        }
    }
}
