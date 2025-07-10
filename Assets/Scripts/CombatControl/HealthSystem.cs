using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public UnityEvent<int, int> OnHealthChanged;
    public UnityEvent OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        Debug.Log($"{gameObject.name} took {damageAmount} damage, has {currentHealth} health left.");

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }
    private void HandleDeath()
    {
        Debug.Log($"{gameObject.name} has been defeated.");
        //Event OnDeath 
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
