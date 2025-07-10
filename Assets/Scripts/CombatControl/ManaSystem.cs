using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ManaSystem : MonoBehaviour
{
    [Header("Mana Settings")]
    [SerializeField] private int maxMP = 100;
    private int currentMP;

    public UnityEvent<int, int> OnMPChanged;

    void Start()
    {
        currentMP = maxMP;
        TimeManager.Instance.OnTimePeriodChanged.AddListener(RestoreFullMPWrapper);
        GetComponent<HealthSystem>().OnDeath.AddListener(RestoreFullMP);
    }
    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimePeriodChanged.RemoveListener(RestoreFullMPWrapper);
        }
    }
    public bool UseMP(int amount)
    {
        if (currentMP >= amount)
        {
            currentMP -= amount;
            OnMPChanged?.Invoke(currentMP, maxMP);
            Debug.Log($"Used {amount} MP, {currentMP} remaining.");
            return true;
        }
        else
        {
            Debug.Log("Not enough Mana Power!");
            return false;
        }
    }

    public void RestoreFullMP()
    {
        currentMP = maxMP;
        OnMPChanged?.Invoke(currentMP, maxMP);
        Debug.Log("Mana Power restored to full!");
    }

    private void RestoreFullMPWrapper(TimePeriod period)
    {
        RestoreFullMP();
    }
}
