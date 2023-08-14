using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFps : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private void Update()
    {
        // Calculate and display the FPS
        fpsText.text = (1.0f / Time.deltaTime).ToString("0");
    }
}
