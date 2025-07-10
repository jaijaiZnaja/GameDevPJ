using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CraftButton : MonoBehaviour
{
    [SerializeField] private CraftingRecipe recipeToCraft;

    private Button craftButton;

    void Start()
    {
        craftButton = GetComponent<Button>();
        craftButton.onClick.AddListener(OnCraftButtonPressed);
        InventoryManager.Instance.OnInventoryChanged.AddListener(UpdateButtonState);
        
        UpdateButtonState();
    }
    
    private void OnEnable()
    {
        UpdateButtonState();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged.RemoveListener(UpdateButtonState);
        }
    }
    private void UpdateButtonState()
    {
        if (recipeToCraft != null && craftButton != null)
        {
            craftButton.interactable = InventoryManager.Instance.HasItems(recipeToCraft.ingredients);
        }
    }
    private void OnCraftButtonPressed()
    {
        if (recipeToCraft != null)
        {
            CraftingSystem.Instance.Craft(recipeToCraft);
        }
    }
}
