using UnityEngine;

public static class LocalSavedDataUtility
{
    // Properties to get and set data using PlayerPrefs

    public static int DailyPomodoroGoal
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.DailyPomodoroGoalKey, defaultValue: 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.DailyPomodoroGoalKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int BreakDuration
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.BreakDurationKey, defaultValue: 0);
        set
        {
            PlayerPrefs.GetInt(PlayerPrefKeys.BreakDurationKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int PomodoroDuration
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.PomodoroDurationKey, defaultValue: 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.PomodoroDurationKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int CompletedPomodoros
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.CompletedPomodorosKey, defaultValue: 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.CompletedPomodorosKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int HarvestableCredits
    {
        get => PlayerPrefs.GetInt(PlayerPrefKeys.HarvestableCreditsKey, defaultValue: 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.HarvestableCreditsKey, value);
            PlayerPrefs.Save();
        }
    }

}
