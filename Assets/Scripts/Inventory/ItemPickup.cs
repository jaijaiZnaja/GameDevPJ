using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    
    [SerializeField] private int quantity = 1;

    public void SetQuantity(int amount)
    {
        this.quantity = amount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemData == null)
            {
                Debug.LogError($"ItemPickup on {gameObject.name} no item Data", this);
                return;
            }
            bool success = InventoryManager.Instance.AddItem(itemData, quantity);
            if (success)
            {
                Destroy(gameObject);
            }
        }
    }
}
