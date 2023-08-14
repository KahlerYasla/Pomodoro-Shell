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
