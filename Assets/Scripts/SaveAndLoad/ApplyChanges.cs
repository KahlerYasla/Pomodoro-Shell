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

        FillTheDots.dotIndex = 0;

        // reload the scene
        ReloadScene();
    }

    private void ReloadScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
