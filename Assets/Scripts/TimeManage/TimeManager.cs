using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    // current time.
    public int CurrentDay { get; private set; } = 1; // Start at Day 1
    public DayOfWeek CurrentDayOfWeek { get; private set; } = DayOfWeek.Mon; // Start on Monday
    public TimePeriod CurrentTimePeriod { get; private set; } = TimePeriod.Morning; // Start Morning
    public UnityEvent<TimePeriod> OnTimePeriodChanged;

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
        if (OnTimePeriodChanged == null)
        {
            OnTimePeriodChanged = new UnityEvent<TimePeriod>();
        }
    }
    public void AdvanceTime()
    {
        CurrentTimePeriod++;

        if (CurrentTimePeriod > TimePeriod.Evening)
        {
            CurrentTimePeriod = TimePeriod.Morning;
            AdvanceDay();
        }
        
        Debug.Log($"Time advanced to: Day {CurrentDay}, {CurrentDayOfWeek}, {CurrentTimePeriod}");

        OnTimePeriodChanged.Invoke(CurrentTimePeriod);
    }

    // update game days (day --> week)

    private void AdvanceDay()
    {
        CurrentDay++;
        CurrentDayOfWeek++;
        if (CurrentDayOfWeek > DayOfWeek.Sun)
        {
            CurrentDayOfWeek = DayOfWeek.Mon;
        }
    }
}
