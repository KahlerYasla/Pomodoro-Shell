using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerColorChanging : MonoBehaviour
{
    private Renderer _objectRenderer;
    private Material _objectMaterial;
    private float _timer;
    private float _colorChangeInterval = 1.5f;

    private void Start()
    {
        // Get the Renderer component of the object
        _objectRenderer = GetComponent<Renderer>();
        // Get the material of the object
        _objectMaterial = _objectRenderer.material;

        // Initialize the timer
        _timer = 0f;
    }

    private void Update()
    {
        // Increment the timer
        _timer += Time.deltaTime;

        // Check if it's time to change the color
        if (_timer >= _colorChangeInterval)
        {
            // Generate a random color
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Assign the random color to the material's emission color
            _objectMaterial.SetColor("_EmissionColor", randomColor);

            // Reset the timer
            _timer = 0f;
        }
    }
}
