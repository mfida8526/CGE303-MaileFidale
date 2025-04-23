using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//must include this to use the Slider
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{

    //slider for the healthbar
    public Slider slider;

    //gradient for the health bar
    public Gradient gradient;

    //image for the fill of the health bar
    public Image fill;

    //function to set the current value of the slider
    public void SetValue(float value)
    {
        //set the value of the slider
        slider.value = value;

        //set the color of the fill of the slider
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //function to set the max value of the slider
    public void SetMaxValue(float value)
    {
        //set the max value of the slider
        slider.maxValue = value;

        //set the current value of the slider to the max value
        slider.value = value;

        //set the color of the fill of the slider
        fill.color = gradient.Evaluate(1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
