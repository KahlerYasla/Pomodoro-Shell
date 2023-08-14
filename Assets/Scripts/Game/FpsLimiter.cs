using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsLimiter : MonoBehaviour
{
    private void Start()
    {
        // Set the target frame rate after 1 second the game starts 
        Invoke("SetTargetFrameRate", 1f);
    }

    private void SetTargetFrameRate()
    {
        // Set the target frame rate to 60
        Application.targetFrameRate = 60;
    }
}
