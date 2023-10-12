using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneReloader
{
    public static TMPro.TMP_Text TimerText;

    public static string SavedTimerText = LocalSavedDataUtility.PomodoroDuration >= 10 ?
     LocalSavedDataUtility.PomodoroDuration.ToString() + ":00" :
      "0" + LocalSavedDataUtility.PomodoroDuration.ToString() + ":00";


    public static void ReloadScene(bool resetTimer = true)
    {
        TimerText = GameObject.Find("Time").GetComponent<TMPro.TMP_Text>();

        if (!resetTimer)
            SavedTimerText = TimerText.text;

        else
            SavedTimerText = LocalSavedDataUtility.PomodoroDuration >= 10 ?
        LocalSavedDataUtility.PomodoroDuration.ToString() + ":00" :
            "0" + LocalSavedDataUtility.PomodoroDuration.ToString() + ":00";

        FillTheDots.dotIndex = 0;

        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.buildIndex);

    }
}
