using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [Header("Wand Settings")]
    [SerializeField] private ItemData wandItemData;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int mpCost = 10;

    private float nextFireTime = 0f;
    private UI_InventoryBar inventoryBar;
    private PlayerMovement playerMovement;
    private ManaSystem manaSystem;

    void Start()
    {
        inventoryBar = FindObjectOfType<UI_InventoryBar>();
        playerMovement = GetComponent<PlayerMovement>();
        manaSystem = GetComponent<ManaSystem>();
    }
    
    void Update()
    {
        if ((UI_Manager.Instance != null && UI_Manager.Instance.IsUIOpen) || Time.time < nextFireTime)
        {
            return;
        }

        if (inventoryBar != null && inventoryBar.GetSelectedItemData() == wandItemData)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (manaSystem.UseMP(mpCost))
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }
    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null || playerMovement == null)
        {
            Debug.LogError("WandController is not set up correctly!");
            return;
        }
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        WandProjectile projectile = projectileGO.GetComponent<WandProjectile>();
        Vector2 fireDirection = playerMovement.isFacingRight ? Vector2.right : Vector2.left;
        projectile.Launch(fireDirection);
        Debug.Log("Fired a projectile!");
    }
}
