using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public ItemData itemData;
    public int quantity;

    public InventorySlot(ItemData data, int amount)
    {
        itemData = data;
        quantity = amount;
    }

    /// <summary>
    /// Adds a specified amount to the slot's quantity.
    /// </summary>
    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
}
