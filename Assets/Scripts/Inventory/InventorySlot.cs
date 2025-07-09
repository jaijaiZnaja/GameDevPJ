using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int quantity;

    public InventorySlot(ItemData data, int amount)
    {
        itemData = data;
        quantity = amount;
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
}
