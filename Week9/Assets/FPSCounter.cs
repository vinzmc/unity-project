using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int frameRange = 60;
    public int AverageFPS { get; private set; }
    public int HighestFPS { get; private set; }
    public int LowestFPS { get; private set; }

    int[] fpsBuffer;
    int fpsBufferIndex;
    // Update is called once per frame
    void Update()
    {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange)
        {
            initializeBuffer();
        }
        UpdateBuffer();
        CalculateFPS();
    }

    void initializeBuffer()
    {
        if (frameRange <= 0)
        {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }

    void UpdateBuffer()
    {
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (fpsBufferIndex >= frameRange)
        {
            fpsBufferIndex = 0;
        }
    }

    void CalculateFPS()
    {
        int sum = 0;
        int highest = 0;
        int lowest = int.MaxValue;
        for (int i = 0; i < frameRange; i++)
        {
            int fps = fpsBuffer[i];
            sum += fps;
            if (fps > highest)
            {
                highest = fps;
            }
            AverageFPS = (int)(float)(sum / frameRange);
            HighestFPS = highest;
            LowestFPS = lowest;
        }
    }
}
