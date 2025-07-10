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
    private PlayerMovement playerMovement;

    [Header("Item Dropping")]
    [SerializeField] private float dropDistance = 1.5f;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        InventoryManager.Instance.OnInventoryChanged.AddListener(Redraw);
        CreateSlots();
        Redraw();
        UpdateSelectionVisuals();
    }

    private void Update()
    {
        if (UI_Manager.Instance != null && UI_Manager.Instance.IsUIOpen)
            return;

        HandleSlotSelectionInput();
        HandleDropItemInput();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged.RemoveListener(Redraw);
        }
    }
    public ItemData GetSelectedItemData()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < InventoryManager.Instance.inventorySlots.Count)
        {
            return InventoryManager.Instance.inventorySlots[selectedSlotIndex].itemData;
        }
        return null;
    }

    private void HandleSlotSelectionInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            selectedSlotIndex -= (int)Mathf.Sign(scroll);
            if (selectedSlotIndex < 0) selectedSlotIndex = slotsToDisplay - 1;
            if (selectedSlotIndex >= slotsToDisplay) selectedSlotIndex = 0;
            UpdateSelectionVisuals();
        }

        for (int i = 0; i < slotsToDisplay; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                int keyNumber = (i + 1) % 10;
                selectedSlotIndex = (keyNumber == 0) ? 9 : keyNumber - 1;

                UpdateSelectionVisuals();
                return;
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
                GameObject newSlotGO = Instantiate(slotPrefab, slotContainer);
                UI_InventorySlot newSlot = newSlotGO.GetComponent<UI_InventorySlot>();
                uiSlots.Add(newSlot);
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
    private void HandleDropItemInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ItemData itemToDrop = GetSelectedItemData();

            if (itemToDrop != null && itemToDrop.pickupPrefab != null)
            {
                Vector2 dropDirection = playerMovement.isFacingRight ? Vector2.right : Vector2.left;
                Vector2 spawnPosition = (Vector2)playerMovement.transform.position + (dropDirection * dropDistance);
                Instantiate(itemToDrop.pickupPrefab, spawnPosition, Quaternion.identity);
            
                InventoryManager.Instance.RemoveItem(selectedSlotIndex, 1);
            }
        }
    }
}
