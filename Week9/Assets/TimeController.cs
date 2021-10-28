using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Slider mainSlider;
    public Text timeScaleLabel;

    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChangeCheck()
    {
        Time.timeScale = mainSlider.value;
        timeScaleLabel.text = "(" + mainSlider.value.ToString() + "x)";
    }
}
