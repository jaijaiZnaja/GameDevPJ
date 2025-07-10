using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI Elements")]
    [SerializeField] public Image icon;
    [SerializeField] public TextMeshProUGUI quantityText;

    [Header("Selection Visuals")]
    [SerializeField] private Image selectionIndicator;
    public int SlotIndex { get; private set; }

    public void Initialize(int index) 
    {
        SlotIndex = index;
    }

    private void Awake()
    {
        SetSelected(false); 
    }

    public void UpdateSlot(InventorySlot slot)
    {
        if (slot.itemData != null && slot.quantity > 0)
        {
            icon.sprite = slot.itemData.icon;
            icon.enabled = true;
            quantityText.enabled = slot.quantity > 1;
            quantityText.text = slot.quantity.ToString();
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
            quantityText.enabled = false;
        }
    }

    public void SetSelected(bool isSelected)
    {
        if (selectionIndicator != null)
        {
            selectionIndicator.enabled = isSelected;
        }
    }

    //  Drag and Drop Logic 

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (InventoryManager.Instance.inventorySlots[SlotIndex].itemData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            DragItem.Instance.ShowIcon(icon.sprite); // แสดงไอเทมที่กำลังลาก
            icon.enabled = false; // ซ่อนไอคอนที่ช่องเดิม
            quantityText.enabled = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragItem.Instance.HideIcon(); 
        UpdateSlot(InventoryManager.Instance.inventorySlots[SlotIndex]); 
    }

    public void OnDrop(PointerEventData eventData)
    {
        UI_InventorySlot sourceSlot = eventData.pointerDrag.GetComponent<UI_InventorySlot>();

        if (sourceSlot != null && sourceSlot != this)
        {
            InventoryManager.Instance.SwapSlots(sourceSlot.SlotIndex, this.SlotIndex);
        }
    }
}
