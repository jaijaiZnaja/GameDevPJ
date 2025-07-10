using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MPBar : MonoBehaviour
{
    [SerializeField] private Image mpFillImage;
    [SerializeField] private ManaSystem playerMPSystem;

    void Start()
    {
        if (playerMPSystem == null)
        {
            playerMPSystem = FindObjectOfType<ManaSystem>();
        }
        if (playerMPSystem != null)
        {
            playerMPSystem.OnMPChanged.AddListener(UpdateMPBar);
            UpdateMPBar(100, 100);
        }
    }

    private void OnDestroy()
    {
        if (playerMPSystem != null)
        {
            playerMPSystem.OnMPChanged.RemoveListener(UpdateMPBar);
        }
    }

    private void UpdateMPBar(int currentMP, int maxMP)
    {
        if (mpFillImage != null)
        {
            mpFillImage.fillAmount = (float)currentMP / maxMP;
        }
    }
}
