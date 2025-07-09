using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryBar : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotContainer;
    [Tooltip("จำนวนช่องที่จะแสดงผลในแถบ Inventory Bar")]
    [SerializeField] private int slotsToDisplay = 10;

    private List<UI_InventorySlot> uiSlots = new List<UI_InventorySlot>();

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged.AddListener(Redraw);
        CreateSlots();
        Redraw();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged.RemoveListener(Redraw);
        }
    }
    private void CreateSlots()
    {
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }
        uiSlots.Clear();

        for (int i = 0; i < slotsToDisplay; i++)
        {
            if (i < InventoryManager.Instance.inventorySlots.Count)
            {
                GameObject newSlot = Instantiate(slotPrefab, slotContainer);
                uiSlots.Add(newSlot.GetComponent<UI_InventorySlot>());
            }
        }
    }
    public void Redraw()
    {
        for (int i = 0; i < uiSlots.Count; i++)
        {
            uiSlots[i].UpdateSlot(InventoryManager.Instance.inventorySlots[i]);
        }
    }
}
