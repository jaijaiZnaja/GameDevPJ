using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory/Item Data")]
public class ItemData : MonoBehaviour
{
    [Header("Item Information")]
    public string itemName;
    public Sprite icon;
    [TextArea(4, 8)]
    public string description;

    [Header("Stacking Information")]
    public int maxStackSize = 1;
}
