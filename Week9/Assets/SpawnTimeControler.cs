using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTimeControler : MonoBehaviour
{
    public Slider mainSlider;
    public Text spawnTimeLabel;
    public NucleonSpawner spawner;

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
        float temp = mainSlider.value * 0.01f;
        spawner.timeBetweenSpawns = temp;
        spawnTimeLabel.text = "(" + temp.ToString() + "x)";
    }
}
