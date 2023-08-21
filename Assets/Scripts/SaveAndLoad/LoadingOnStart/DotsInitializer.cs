using UnityEngine;

public class DotsInitializer : MonoBehaviour
{
    public GameObject resumingDotPrefab;
    public GameObject circlesParent;
    private int yPositionOfDots = 0;
    private readonly int _gapBetweenDots = 60;

    private void Start()
    {
        // wait for 1 second before initializing dots
        Invoke("InitializeDots", 0.3f);
        Invoke("FillCompletedDots", 0.5f);
    }

    private void InitializeDots()
    {
        // get the number of daily pomodoro goal
        int dailyPomodoroGoal = LocalSavedDataUtility.DailyPomodoroGoal;

        // set the starting position of the dots based on the number of pomodoros and centeralize the dots
        int startingPositionX = dailyPomodoroGoal % 2 == 0 ? -((dailyPomodoroGoal / 2) - 1) * _gapBetweenDots : -(dailyPomodoroGoal / 2) * _gapBetweenDots;

        if (dailyPomodoroGoal > 20)
        {
            startingPositionX = 20 % 2 == 0 ? -((20 / 2) - 1) * _gapBetweenDots : -(20 / 2) * _gapBetweenDots;
            startingPositionX -= _gapBetweenDots / 2;
        }

        for (int i = 0; i < dailyPomodoroGoal; i++)
        {
            if (i != 0 && i % 20 == 0)
            {
                yPositionOfDots -= _gapBetweenDots;

                int remaining = dailyPomodoroGoal - i;
                if (dailyPomodoroGoal - i < 20)
                {
                    startingPositionX = remaining % 2 == 0 ? -((remaining / 2) - 1) * _gapBetweenDots : -(remaining / 2) * _gapBetweenDots;
                }
                else
                {
                    startingPositionX = 20 % 2 == 0 ? -((20 / 2) - 1) * _gapBetweenDots : -(20 / 2) * _gapBetweenDots;
                }
                startingPositionX -= _gapBetweenDots / 2;
            }
            // instantiate the dot prefab
            GameObject dot = Instantiate(resumingDotPrefab, circlesParent.transform);

            // set the dot child of the circles parent
            dot.transform.SetParent(circlesParent.transform);


            dot.transform.localPosition = new Vector3(startingPositionX, yPositionOfDots, 0);
            startingPositionX += _gapBetweenDots;


            // set the dot name
            dot.name = "Dot" + i.ToString();

        }
    }

    private void FillCompletedDots()
    {
        // fill the dots according to the completed pomodoros
        int completedPomodoros = LocalSavedDataUtility.CompletedPomodoros;
        for (int i = 0; i < completedPomodoros; i++)
        {
            FillTheDots.FillADot();
        }
    }
}
