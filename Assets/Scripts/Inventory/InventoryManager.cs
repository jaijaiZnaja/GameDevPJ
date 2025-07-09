using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Settings")]
    [SerializeField] private int inventorySize = 20; // 

    // list of all slots in the inventory.
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    // Event to notify
    public UnityEvent OnInventoryChanged;

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

        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlots.Add(new InventorySlot(null, 0));
        }
    }

    public bool AddItem(ItemData itemToAdd, int amount)
    {

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData == itemToAdd && slot.quantity < itemToAdd.maxStackSize)
            {
                int spaceAvailable = itemToAdd.maxStackSize - slot.quantity;
                int amountToAdd = Mathf.Min(amount, spaceAvailable);

                slot.AddQuantity(amountToAdd);
                amount -= amountToAdd;

                // If we've added all the items, we're done.
                if (amount <= 0)
                {
                    Debug.Log($"Added {amountToAdd} {itemToAdd.itemName} to an existing stack.");
                    OnInventoryChanged?.Invoke(); // Notify listeners
                    return true;
                }
            }
        }

        // --- New Slot Logic ---
        // If there are still items left to add, find an empty slot.
        if (amount > 0)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.itemData == null)
                {
                    // Can only add up to the max stack size in a new slot
                    int amountToAdd = Mathf.Min(amount, itemToAdd.maxStackSize);

                    slot.itemData = itemToAdd;
                    slot.quantity = amountToAdd;
                    amount -= amountToAdd;
                    
                    Debug.Log($"Added {amountToAdd} {itemToAdd.itemName} to a new slot.");

                    // If we've added all the items, we're done.
                    if (amount <= 0)
                    {
                        OnInventoryChanged?.Invoke(); // Notify listeners
                        return true;
                    }
                }
            }
        }

        // If we reach here, it means the inventory is full.
        if (amount > 0)
        {
            Debug.LogWarning($"Inventory is full! Could not add {amount} of {itemToAdd.itemName}.");
            OnInventoryChanged?.Invoke(); // Still invoke, because some items might have been added.
            return false;
        }

        return true;
    }
}
