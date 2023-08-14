using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadOnStart : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dailyPomodoroGoal, breakDuration, pomodoroDuration, timer;

    // Start is called before the first frame update
    void Start()
    {
        // load the timer data from PlayerPrefs
        dailyPomodoroGoal.text = LocalSavedDataUtility.DailyPomodoroGoal.ToString();
        breakDuration.text = LocalSavedDataUtility.BreakDuration.ToString();
        pomodoroDuration.text = LocalSavedDataUtility.PomodoroDuration.ToString();

        // timer.text value is in minutes. Change it to format of mm:ss
        int timerInt = LocalSavedDataUtility.PomodoroDuration;
        timer.text = timerInt >= 10 ? timerInt.ToString() + ":00" : "0" + timerInt.ToString() + ":00";

        // debug log the timer data
        // Debug.Log(GetTimerData());

        // debug log the indexToItemNameDictionary
        print(Dictionaries.IndexToItemName[0]);

    }

    private string GetTimerData()
    {
        string timerData = string.Format("Loading Data\n---------------\nDaily Pomodoro Goal: {0}\nBreak Duration: {1}\nPomodoro Duration: {2}",
            LocalSavedDataUtility.DailyPomodoroGoal,
            LocalSavedDataUtility.BreakDuration,
            LocalSavedDataUtility.PomodoroDuration);
        return timerData;
    }
}
