using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyChanges : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dailyPomodoroGoal, breakDuration, pomodoroDuration;

    // when apply changes button is pressed, save the changes to the timer data
    public void ApplyChangesButton()
    {
        // save the changes to PlayerPrefs
        LocalSavedDataUtility.DailyPomodoroGoal = int.Parse(dailyPomodoroGoal.text);
        LocalSavedDataUtility.PomodoroDuration = int.Parse(pomodoroDuration.text);
        LocalSavedDataUtility.BreakDuration = int.Parse(breakDuration.text);

        // reload the scene
        SceneReloader.ReloadScene(resetTimer: true);
    }

}
