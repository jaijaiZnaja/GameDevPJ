using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] public UI_InventoryPanel inventoryPanel; // ลาก Panel ของ Inventory เต็มมาใส่

    public bool IsUIOpen { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.Toggle();
            UpdateGameState();
        }
    }

    private void UpdateGameState()
    {
        IsUIOpen = inventoryPanel.gameObject.activeSelf;

        if (IsUIOpen)
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
        }
    }
}
