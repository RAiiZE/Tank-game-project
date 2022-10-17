using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // references
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    // function to set the max health on the slider and fill it with the color
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    // Function to set the health and make the slider update
    public void SetHeath(int health)
    {
        slider.value = Mathf.Lerp(slider.value, health, Time.deltaTime * 7);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}
