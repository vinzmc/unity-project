using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private string status = "up";
    public GameObject referenceObject;
    Clock referencedScript = null;
    void Start()
    {
        referencedScript = referenceObject.GetComponent<Clock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Clicked();
        }
    }

    void Clicked()
    {
        buttonEffect();
        if (referencedScript.mode == Clock.Mode.Stopwatch)
        {
            if (!referencedScript.running)
            {
                referencedScript.running = true;
            }
            else
            {
                referencedScript.running = false;
            }
        }
    }

    void buttonEffect()
    {
        if (status == "up")
        {
            status = "down";
            transform.position = new Vector3(3.91f, 0f, 3.71f);
            transform.localScale = new Vector3(-0.36f, 0.2f, 1f);
        }
        else
        {
            status = "up";
            transform.position = new Vector3(4f, 0f, 3.8f);
            transform.localScale = new Vector3(-0.36f, 0.315f, 1f);
        }
    }
}
