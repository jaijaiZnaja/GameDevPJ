using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragItem : MonoBehaviour
{
    public static DragItem Instance { get; private set; }

    [SerializeField] private Image itemIcon;
    private RectTransform rectTransform;

    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        HideIcon();
    }

    public void ShowIcon(Sprite sprite)
    {
        itemIcon.sprite = sprite;
        itemIcon.enabled = true;
        gameObject.SetActive(true);
    }

    public void HideIcon()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {

        if (gameObject.activeSelf)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

}

