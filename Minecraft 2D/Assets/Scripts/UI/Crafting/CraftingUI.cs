using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private RectTransform content;

    public void AddRecipe(CraftingRecipe recipe, PlayerCrafting playerCrafting)
    {
        var newRecipe = Instantiate(GameAssets.i.craftingSlotPrefab, content);
        newRecipe.SetItem(recipe.resoultItem.sprite, recipe.amount);
        newRecipe.recipeID = recipe.resoultItem.ID;
        newRecipe.playerCrafting = playerCrafting;
    }
}
