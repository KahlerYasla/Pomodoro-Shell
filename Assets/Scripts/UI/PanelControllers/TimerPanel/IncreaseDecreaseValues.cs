using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseDecreaseValues : MonoBehaviour
{
    private int _value;

    [SerializeField] int minValue, maxValue;

    public void IncreaseValue(Button button)
    {
        // get the sibling of value text which is TextMeshProUGUI
        TMPro.TextMeshProUGUI valueText = button.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();

        // if the value is less than the max value
        if (int.Parse(valueText.text) < maxValue)
        {
            // increase the value
            _value = int.Parse(valueText.text);
            _value++;
        }
        else if (int.Parse(valueText.text) == maxValue)
        {
            return;
        }

        // set the value text to the new value
        valueText.text = _value.ToString();
    }

    public void DecreaseValue(Button button)
    {
        // get the sibling of value text which is TextMeshProUGUI
        TMPro.TextMeshProUGUI valueText = button.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();

        // if the value is greater than the min value
        if (int.Parse(valueText.text) > minValue)
        {
            // decrease the value
            _value = int.Parse(valueText.text);
            _value--;
        }
        else if (int.Parse(valueText.text) == minValue)
        {
            return;
        }

        // set the value text to the new value
        valueText.text = _value.ToString();
    }
}
