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

        InstantiateCustomizationObjectsAsync();
    }

    private async void InstantiateCustomizationObjectsAsync()
    {
        // get the profile data
        DatabaseManager databaseManager = new();

        ProfileModel profileData = await databaseManager.GetProfileModelAsync();

        // get the unlocked items
        string[] ActiveItems = profileData.ActiveItems.Split(',');

        // instantiate the active items
        for (int i = 0; i < ActiveItems.Length; i++)
        {
            int ThemeIndex = int.Parse(ActiveItems[i]) / NoteBook.CountOfItemsPerTheme();
            int ItemIndex = int.Parse(ActiveItems[i]) % NoteBook.CountOfItemsPerTheme();

            // instantiate the item
            GameObject item = Instantiate(Resources.Load("Prefabs/CustomizableObjects/" + ThemeIndex.ToString() + "/"
            + Dictionaries.IndexToItemName[ItemIndex]) as GameObject, GameObject.Find("GameObjects").transform.Find("CustomizableObjects"));

        }
    }

}
