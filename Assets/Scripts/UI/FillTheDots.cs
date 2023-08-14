using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTheDots : MonoBehaviour
{
    public static GameObject CompletedDotPrefab, DotsParent;
    public static int dotIndex = 0;

    void Start()
    {
        CompletedDotPrefab = Resources.Load<GameObject>("Prefabs/Dots/CompletedDot");
        DotsParent = GameObject.Find("Circles");
    }

    public static void FillADot()
    {
        // get the daily pomodoro goal
        int dailyPomodoroGoal = LocalSavedDataUtility.DailyPomodoroGoal;

        #region Harvestable Credits ----------------------------------------------------
        // Get how many minutes per pomodoro
        int minutesPerPomodoro = LocalSavedDataUtility.PomodoroDuration;

        // Check if the "HarvestableCredits" key exists in PlayerPrefs
        if (LocalSavedDataUtility.HarvestableCredits > 0)
        {
            // Key exists, so increase the value by 1
            LocalSavedDataUtility.HarvestableCredits += minutesPerPomodoro;
        }
        else
        {
            // Key does not exist, set it to 1
            LocalSavedDataUtility.HarvestableCredits = 1;
        }
        #endregion Harvestable Credits ----------------------------------------------------

        if (dotIndex == dailyPomodoroGoal)
        {
            return;
        }

        // change the dot to completed dot
        DotsParent.transform.GetChild(dotIndex).GetComponent<Image>().sprite = CompletedDotPrefab.GetComponent<Image>().sprite;

        // set the completed pomodoros player pref 
        LocalSavedDataUtility.CompletedPomodoros = dotIndex + 1;

        // increase the dot index
        dotIndex++;
    }
}