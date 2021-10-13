using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lightorobj;
    public GameObject lightorobj2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lightorobj.SetActive(!lightorobj.activeSelf);
            lightorobj2.SetActive(!lightorobj2.activeSelf);
        }

    }
}
