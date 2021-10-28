using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hoursTransform, minutesTransform, secondsTransform;
    public bool continous = true;
    public bool running = false;
    public bool reset = false;
    public enum Mode { Stopwatch, Jam  }
    public Mode mode;


    private TimeSpan timeTimer = TimeSpan.Zero;
    private DateTime timeTimerDiscrete = new DateTime(2021, 10, 1, 0, 0, 0, 0);

    const float
        degreePerHour = 30f,
        degreePerMinute = 6f,
        degreePerSecond = 6f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mode == Mode.Jam)
        {
            running = false;
            resetStopwatch();
            ClockMode();
        }
        else
        {
            StopwatchMode();
        }
    }

    void StopwatchMode()
    {
        if (running)
        {
            TimeSpan increase = TimeSpan.FromSeconds(Time.deltaTime);
            timeTimer += increase;
            timeTimerDiscrete = timeTimerDiscrete.Add(increase);
            if (continous)
            {
                hoursTransform.localRotation =
                    Quaternion.Euler(0f, ((float)timeTimer.TotalHours) * degreePerHour, 0f);
                minutesTransform.localRotation =
                    Quaternion.Euler(0f, ((float)timeTimer.TotalMinutes) * degreePerMinute, 0f);
                secondsTransform.localRotation =
                    Quaternion.Euler(0f, ((float)timeTimer.TotalSeconds) * degreePerSecond, 0f);
            }
            else
            {
                hoursTransform.localRotation =
                    Quaternion.Euler(0f, timeTimerDiscrete.Hour * degreePerHour, 0f);
                minutesTransform.localRotation =
                    Quaternion.Euler(0f, timeTimerDiscrete.Minute * degreePerMinute, 0f);
                secondsTransform.localRotation =
                    Quaternion.Euler(0f, timeTimerDiscrete.Second * degreePerSecond, 0f);
            }
        }
        if (reset)
        {
            resetStopwatch();
        }
    }

    void resetStopwatch()
    {
        timeTimer = TimeSpan.Zero;
        timeTimerDiscrete = new DateTime(2021, 10, 1, 0, 0, 0, 0);
        reset = false;
    }
    void ClockMode() //continous
    {

        if (continous)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;
            hoursTransform.localRotation =
            Quaternion.Euler(0f, ((float)time.TotalHours) * degreePerHour, 0f);
            minutesTransform.localRotation =
                Quaternion.Euler(0f, ((float)time.TotalMinutes) * degreePerMinute, 0f);
            secondsTransform.localRotation =
                Quaternion.Euler(0f, ((float)time.TotalSeconds) * degreePerSecond, 0f);
        }
        else
        {
            DateTime time = DateTime.Now;
            hoursTransform.localRotation =
                Quaternion.Euler(0f, time.Hour * degreePerHour, 0f);
            minutesTransform.localRotation =
                Quaternion.Euler(0f, time.Minute * degreePerMinute, 0f);
            secondsTransform.localRotation =
                Quaternion.Euler(0f, time.Second * degreePerSecond, 0f);
        }
    }

    // void CheckUpdate()
    // {
    //     if (continous)
    //     {
    //         UpdateContinous();
    //     }
    //     else
    //     {
    //         UpdateDiscrete();
    //     }
    // }

    // void UpdateContinous()
    // {
    //     TimeSpan time = DateTime.Now.TimeOfDay;
    //     hoursTransform.localRotation =
    //         Quaternion.Euler(0f, ((float)time.TotalHours) * degreePerHour, 0f);
    //     minutesTransform.localRotation =
    //         Quaternion.Euler(0f, ((float)time.TotalMinutes) * degreePerMinute, 0f);
    //     secondsTransform.localRotation =
    //         Quaternion.Euler(0f, ((float)time.TotalSeconds) * degreePerSecond, 0f);
    // }
    // void UpdateDiscrete()
    // {
    //     DateTime time = DateTime.Now;
    //     hoursTransform.localRotation =
    //         Quaternion.Euler(0f, time.Hour * degreePerHour, 0f);
    //     minutesTransform.localRotation =
    //         Quaternion.Euler(0f, time.Minute * degreePerMinute, 0f);
    //     secondsTransform.localRotation =
    //         Quaternion.Euler(0f, time.Second * degreePerSecond, 0f);
    // }
}