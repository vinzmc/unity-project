using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeThreeMauriceMarvin : MonoBehaviour
{
    public int spinSpeed;
    public Vector3 RotateAmount;
    float timeCounter = 0;
    float modifier;
    // Start is called before the first frame update
    void Start()
    {
        RotateAmount = new Vector3(0.0f, -55.0f, 0.0f);
        modifier = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        transform.Rotate(0, -57.3f * Time.deltaTime, 0);

        float x = Mathf.Cos(timeCounter) * modifier;
        float y = 0;
        float z = Mathf.Sin(timeCounter) * modifier;

        transform.position = new Vector3(x, y, z);
    }
}
