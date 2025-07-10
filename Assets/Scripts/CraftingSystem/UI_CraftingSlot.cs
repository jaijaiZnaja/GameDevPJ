using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_CraftingSlot : MonoBehaviour
{
    [Header("Recipe Data")]
    [SerializeField] private CraftingRecipe recipe;

    [Header("UI Elements")]
    [SerializeField] private Image resultIcon;
    [SerializeField] private TextMeshProUGUI resultNameText;
    [SerializeField] private TextMeshProUGUI ingredientsText;
    [SerializeField] private Button craftButton;

    void Start()
    {
        InventoryManager.Instance.OnInventoryChanged.AddListener(UpdateCraftButton);
        craftButton.onClick.AddListener(OnCraftButtonPressed);

        DisplayRecipe();
        UpdateCraftButton();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged.RemoveListener(UpdateCraftButton);
        }
    }
    private void DisplayRecipe()
    {
        if (recipe == null) return;

        resultIcon.sprite = recipe.result.icon;
        resultNameText.text = recipe.result.itemName;

        string ingredientsList = "Requires:\n";
        foreach (var ingredient in recipe.ingredients)
        {
            ingredientsList += $"{ingredient.quantity}x {ingredient.item.itemName}\n";
        }
        ingredientsText.text = ingredientsList;
    }

    private void UpdateCraftButton()
    {
        if (recipe != null)
        {
            craftButton.interactable = InventoryManager.Instance.HasItems(recipe.ingredients);
        }
    }
    private void OnCraftButtonPressed()
    {
        if (recipe != null)
        {
            CraftingSystem.Instance.Craft(recipe);
        }
    }
}
