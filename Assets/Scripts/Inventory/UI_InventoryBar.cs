using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryBar : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotContainer;
    [Tooltip("How many Slots that show on Inventory Bar")]
    [SerializeField] private int slotsToDisplay = 10;

    private List<UI_InventorySlot> uiSlots = new List<UI_InventorySlot>();
    private int selectedSlotIndex = 0;

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged.AddListener(Redraw);
        CreateSlots();
        Redraw();
        UpdateSelectionVisuals();
    }

    private void Update()
    {
        HandleSlotSelectionInput();
        HandleUseItemInput();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged.RemoveListener(Redraw);
        }
    }

    private void HandleSlotSelectionInput()
    {
        //Scroll Wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            selectedSlotIndex -= (int)Mathf.Sign(scroll);

            if (selectedSlotIndex < 0) selectedSlotIndex = slotsToDisplay - 1;
            if (selectedSlotIndex >= slotsToDisplay) selectedSlotIndex = 0;
            
            UpdateSelectionVisuals();
        }

        // number 1-0
        for (int i = 0; i < slotsToDisplay; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlotIndex = (i + 1) % 10 == 0 ? 9 : i;
                UpdateSelectionVisuals();
                return; 
            }
        }
    }

    private void HandleUseItemInput()
    {
        // use items
        if (Input.GetMouseButtonDown(0))
        {
            InventorySlot slotData = InventoryManager.Instance.inventorySlots[selectedSlotIndex];
            if (slotData.itemData != null)
            {
                Debug.Log($"Used item: {slotData.itemData.itemName}");
            }
            else
            {
                Debug.Log("Selected slot is empty.");
            }
        }
    }

    private void UpdateSelectionVisuals()
    {
        for (int i = 0; i < uiSlots.Count; i++)
        {
            uiSlots[i].SetSelected(i == selectedSlotIndex);
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
