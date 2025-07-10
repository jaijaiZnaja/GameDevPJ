using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    public Sprite icon;
    [TextArea(4, 8)]
    public string description;

    [Header("Stacking Information")]
    public int maxStackSize = 1;

    [Header("World Representation")] 
    [Tooltip("DropPrefab")]
    public GameObject pickupPrefab; 
}
