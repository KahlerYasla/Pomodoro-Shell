using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStopTheTimer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timer;
    public Button startButton, stopButton;

    private bool _isTimerRunning = false;
    private Coroutine _timerCoroutine;
    private int timerInt;

    public void StartTimer()
    {
        // if the PomodoroDuration or DailyPomodoroGoal is 0 or less, return and dont start the timer
        if (LocalSavedDataUtility.PomodoroDuration <= 0 || LocalSavedDataUtility.DailyPomodoroGoal <= 0)
        {
            return;
        }

        _isTimerRunning = true;
        _timerCoroutine = StartCoroutine(StartTimerCoroutine());

        startButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        _isTimerRunning = false;
        StopCoroutine(_timerCoroutine);

        startButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
    }

    private IEnumerator StartTimerCoroutine()
    {
        string[] timerText = timer.text.Split(':');
        int minutes = int.Parse(timerText[0]);
        int seconds = int.Parse(timerText[1]);

        while (_isTimerRunning)
        {
            // if seconds is 0, decrease minutes by 1 and set seconds to 59
            if (seconds == 0)
            {
                // if minutes is 0, stop the timer it has reached 00:00
                if (minutes == 0)
                {
                    _isTimerRunning = false;

                    startButton.gameObject.SetActive(true);
                    stopButton.gameObject.SetActive(false);

                    FillTheDots.FillADot();

                    // timer.text value is in minutes. Change it to format of mm:ss
                    timerInt = LocalSavedDataUtility.PomodoroDuration;

                    timer.text = timerInt >= 10 ? timerInt.ToString() + ":00" : "0" + timerInt.ToString() + ":00";

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

                    // show break dialog
                    BreakDialog.Activate();

                    break;
                }
                else
                {
                    minutes--;
                    seconds = 59;
                }
            }
            else
            {
                seconds--;
            }

            timer.text = minutes >= 10 ? minutes.ToString() : "0" + minutes.ToString();
            timer.text += ":";
            timer.text += seconds >= 10 ? seconds.ToString() : "0" + seconds.ToString();

            yield return new WaitForSeconds(1);
        }
    }
}
