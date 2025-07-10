using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Craft(CraftingRecipe recipe)
    {
        if (recipe == null) return;

        // ตรวจสอบว่ามีวัตถุดิบพอหรือไม่
        if (InventoryManager.Instance.HasItems(recipe.ingredients))
        {
            // ลบวัตถุดิบออกจาก Inventory
            InventoryManager.Instance.RemoveItems(recipe.ingredients);
            // เพิ่มของที่ Craft เสร็จแล้วเข้า Inventory
            InventoryManager.Instance.AddItem(recipe.result, recipe.resultQuantity);

            Debug.Log($"Successfully crafted {recipe.result.itemName}");
        }
        else
        {
            Debug.LogWarning("Not enough ingredients to craft!");
        }
    }
}
