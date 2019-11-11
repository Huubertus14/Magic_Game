using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSliderScript : MonoBehaviour
{
    [Header("Reference:")]
    public Slider slider;
    public float sliderSpeed;

    private float goalValue;

    public void InitSlider(float _maxValue, float _currentValue)
    {
        slider.maxValue = _maxValue;
        slider.value = _currentValue;
        goalValue = slider.value;

        if (sliderSpeed == 0)
        {
            sliderSpeed = 0;
        }
    }

    public void SetSlider(float _mana)
    {
        goalValue = _mana;
    }

    private void FixedUpdate()
    {
        slider.value = Mathf.Lerp(slider.value, goalValue, Time.deltaTime * sliderSpeed);
    }
}
