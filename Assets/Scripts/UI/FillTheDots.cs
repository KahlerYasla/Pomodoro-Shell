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