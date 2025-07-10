using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Settings")]
    [SerializeField] private int inventorySize = 20;

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
        bool itemAddedSuccessfully = false;
        int originalAmount = amount;

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData == itemToAdd && slot.quantity < itemToAdd.maxStackSize)
            {
                int spaceAvailable = itemToAdd.maxStackSize - slot.quantity;
                int amountToAdd = Mathf.Min(amount, spaceAvailable);

                slot.AddQuantity(amountToAdd);
                amount -= amountToAdd;
                itemAddedSuccessfully = true;

                if (amount <= 0)
                {
                    break;
                }
            }
        }
        if (amount > 0)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.itemData == null)
                {
                    int amountToAdd = Mathf.Min(amount, itemToAdd.maxStackSize);

                    slot.itemData = itemToAdd;
                    slot.quantity = amountToAdd;
                    amount -= amountToAdd;
                    itemAddedSuccessfully = true;

                    Debug.Log($"Added {amountToAdd} {itemToAdd.itemName} to a new slot.");
                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }
        }
        if (itemAddedSuccessfully)
        {
            Debug.Log($"Successfully added {originalAmount - amount} of {itemToAdd.itemName}.");
            OnInventoryChanged?.Invoke(); // Notify listeners that the inventory changed
        }
        //inventory is full.
        if (amount > 0)
        {
            Debug.LogWarning($"Inventory is full! Could not add the remaining {amount} of {itemToAdd.itemName}.");
        }
        return itemAddedSuccessfully; ;
    }
    public void SwapSlots(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= inventorySlots.Count || indexB < 0 || indexB >= inventorySlots.Count)
        {
            return;
        }
        // Swap Item on list
        InventorySlot temp = inventorySlots[indexA];
        inventorySlots[indexA] = inventorySlots[indexB];
        inventorySlots[indexB] = temp;

        Debug.Log($"Swapped items between slot {indexA} and {indexB}");
        
        // New UI on swap
        OnInventoryChanged?.Invoke();
    }
}
