using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setSliderMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;
        fill.color = gradient.Evaluate(1f);
    }

    public void setSliderValue(int currValue)
    {
        slider.value = currValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
