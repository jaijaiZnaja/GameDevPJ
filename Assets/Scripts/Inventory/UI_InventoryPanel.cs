using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryPanel : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] public GameObject slotPrefab;
    [SerializeField] public Transform mainInventoryContainer; 
    [SerializeField] public Transform hotbarContainer;      

    private List<UI_InventorySlot> mainSlots = new List<UI_InventorySlot>();
    private List<UI_InventorySlot> hotbarSlots = new List<UI_InventorySlot>();

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged.AddListener(RedrawAll);
        CreateSlots();
        RedrawAll();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged.RemoveListener(RedrawAll);
        }
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void CreateSlots()
    {
        // Hotbar (10 slots)
        for (int i = 0; i < 10; i++)
        {
            if (i < InventoryManager.Instance.inventorySlots.Count)
            {
                GameObject newSlot = Instantiate(slotPrefab, hotbarContainer);
                hotbarSlots.Add(newSlot.GetComponent<UI_InventorySlot>());
            }
        }

        //Inventory
        for (int i = 10; i < InventoryManager.Instance.inventorySlots.Count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, mainInventoryContainer);
            mainSlots.Add(newSlot.GetComponent<UI_InventorySlot>());
        }
    }

    private void RedrawAll()
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].UpdateSlot(InventoryManager.Instance.inventorySlots[i]);
        }
        for (int i = 0; i < mainSlots.Count; i++)
        {
            mainSlots[i].UpdateSlot(InventoryManager.Instance.inventorySlots[i + 10]);
        }
    }
}
