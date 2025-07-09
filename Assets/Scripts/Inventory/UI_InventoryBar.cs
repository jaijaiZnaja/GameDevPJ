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
        // --- ใช้ Scroll Wheel ---
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            selectedSlotIndex -= (int)Mathf.Sign(scroll);

            if (selectedSlotIndex < 0) selectedSlotIndex = slotsToDisplay - 1;
            if (selectedSlotIndex >= slotsToDisplay) selectedSlotIndex = 0;
            
            UpdateSelectionVisuals();
        }

        // --- ใช้ปุ่มตัวเลข 1-0 ---
        for (int i = 0; i < slotsToDisplay; i++)
        {
            // KeyCode.Alpha1 คือเลข 1, KeyCode.Alpha0 คือเลข 0
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // แปลงให้เลข 0 อยู่ที่ index 9
                selectedSlotIndex = (i + 1) % 10 == 0 ? 9 : i;
                UpdateSelectionVisuals();
                return; // ออกจากฟังก์ชันเมื่อเจอการกดปุ่ม
            }
        }
    }

    private void HandleUseItemInput()
    {
        // กดคลิกซ้ายเพื่อ "ใช้" ไอเทม
        if (Input.GetMouseButtonDown(0))
        {
            // ดึงข้อมูลไอเทมจากช่องที่เลือก
            InventorySlot slotData = InventoryManager.Instance.inventorySlots[selectedSlotIndex];
            if (slotData.itemData != null)
            {
                // ในอนาคตจะเปลี่ยนเป็น Logic การใช้ของจริงๆ
                Debug.Log($"Used item: {slotData.itemData.itemName}");
                // ตัวอย่าง: inventoryManager.UseItem(selectedSlotIndex);
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
