using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimescalePreset : MonoBehaviour
{
    public Button setButton, slowButton, fastButton, rtButton;
    public InputField mainInputField;

    public TimeController referencedScript;
    private float timeScaleValue;
    private float slowMoValue = 0.5f;
    private float fastMoValue = 2f;
    // Start is called before the first frame update
    void Start()
    {
        mainInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        setButton.onClick.AddListener(delegate { ButtonClick(); });
        slowButton.onClick.AddListener(delegate { SlowMotion(); });
        fastButton.onClick.AddListener(delegate { FastMotion(); });
        rtButton.onClick.AddListener(delegate { realtimeButton(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChangeCheck()
    {
        float temp = 0;
        if (float.TryParse(mainInputField.text, out temp))
        {
            timeScaleValue = temp;
        }
    }

    public void ButtonClick()
    {
        referencedScript.mainSlider.value = timeScaleValue;
    }

    public void SlowMotion()
    {
        referencedScript.mainSlider.value = slowMoValue;
    }

    public void FastMotion()
    {
        referencedScript.mainSlider.value = fastMoValue;
    }

    public void realtimeButton(){
        referencedScript.mainSlider.value = 1f;
    }
}