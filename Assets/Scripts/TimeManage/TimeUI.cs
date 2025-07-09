using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timeText;

    void Start()
    {
        TimeManager.Instance.OnTimePeriodChanged.AddListener(UpdateDisplay);
        UpdateDisplay(TimeManager.Instance.CurrentTimePeriod);
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimePeriodChanged.RemoveListener(UpdateDisplay);
        }
    }

    void UpdateDisplay(TimePeriod period)
    {
        if (timeText != null && TimeManager.Instance != null)
        {
            int day = TimeManager.Instance.CurrentDay;
            DayOfWeek dayOfWeek = TimeManager.Instance.CurrentDayOfWeek;
            TimePeriod timePeriod = TimeManager.Instance.CurrentTimePeriod;

            timeText.text = $"Day {day} | {dayOfWeek} | {timePeriod}";
        }
    }
}
