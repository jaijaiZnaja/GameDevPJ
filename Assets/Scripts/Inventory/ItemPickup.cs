using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemToPickup; // Assign an ItemData asset in the Inspector
        public int amountToPickup = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                bool success = InventoryManager.Instance.AddItem(itemToPickup, amountToPickup);
                if (success)
                {
                    Debug.Log($"Picked up {itemToPickup.itemName}");
                    // Destroy the pickup object after it's collected.
                    Destroy(gameObject);
                }
            }
        }
}
