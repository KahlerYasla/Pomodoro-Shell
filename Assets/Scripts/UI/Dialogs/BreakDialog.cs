using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakDialog : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timer;
    public static GameObject breakDialogPanel;
    public Button startStopButton;

    private Coroutine timerCoroutine;
    private bool isTimerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        // timer.text value is in minutes. Change it to format of mm:ss
        int timerInt = LocalSavedDataUtility.BreakDuration;
        timer.text = timerInt >= 10 ? timerInt.ToString() + ":00" : "0" + timerInt.ToString() + ":00";

        breakDialogPanel = GameObject.Find("BreakDialog");

        // set position of break dialog panel to the center of the screen
        breakDialogPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        breakDialogPanel.SetActive(false);
    }

    public static void Activate()
    {
        breakDialogPanel.SetActive(true);
    }

    public void SkipBreak()
    {
        breakDialogPanel.SetActive(false);
    }

    public void StartStopBreak()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            StopCoroutine(timerCoroutine);

            startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Start The Break";
        }
        else
        {
            isTimerRunning = true;
            timerCoroutine = StartCoroutine(StartTimerCoroutine());

            startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Stop The Break";
        }
    }

    private IEnumerator StartTimerCoroutine()
    {
        string[] timerText = timer.text.Split(':');
        int minutes = int.Parse(timerText[0]);
        int seconds = int.Parse(timerText[1]);

        while (isTimerRunning)
        {
            // if seconds is 0, decrease minutes by 1 and set seconds to 59
            if (seconds == 0)
            {
                // if minutes is 0, stop the timer it has reached 00:00
                if (minutes == 0)
                {

                    isTimerRunning = false;

                    startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Start The Break";

                    // timer.text value is in minutes. Change it to format of mm:ss
                    int timerInt = LocalSavedDataUtility.BreakDuration;
                    timer.text = timerInt >= 10 ? timerInt.ToString() + ":00" : "0" + timerInt.ToString() + ":00";

                    breakDialogPanel.SetActive(false);

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
