using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CraftingUI craftingUI;
    [SerializeField] private PlayerCrafting playerCrafting;

    private static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (CraftingRecipe recipe in GameAssets.Instance.ItemRecipeDictionary.Values)
        {
            craftingUI.AddRecipe(recipe, playerCrafting);
        }
    }
}