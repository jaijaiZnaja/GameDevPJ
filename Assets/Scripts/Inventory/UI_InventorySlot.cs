using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventorySlot : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] public Image icon;
    [SerializeField] public TextMeshProUGUI quantityText;
    [SerializeField] public GameObject selector;

    private void Awake()
    {
        if (selector != null)
        {
            selector.SetActive(false);
        }
    }

    public void UpdateSlot(InventorySlot slot)
    {
        if (slot.itemData != null && slot.quantity > 0)
        {
            // Update icon
            icon.sprite = slot.itemData.icon;
            icon.enabled = true;

            if (slot.quantity > 1)
            {
                quantityText.text = slot.quantity.ToString();
                quantityText.enabled = true;
            }
            else
            {
                quantityText.enabled = false;
            }
        }
        else
        {
            // empty slot
            icon.sprite = null;
            icon.enabled = false;
            quantityText.enabled = false;
        }
    }

    //select visibility

    public void SetSelected(bool isSelected)
    {
        if (selector != null)
        {
            selector.SetActive(isSelected);
        }
    }


}
