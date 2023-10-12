using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetCompleted : MonoBehaviour
{
    public void ResetCompletedButton()
    {
        // reset the completed pomodoros to 0
        LocalSavedDataUtility.CompletedPomodoros = 0;

        FillTheDots.dotIndex = 0;

        // reload the scene
        SceneReloader.ReloadScene(resetTimer: true);
    }
}
