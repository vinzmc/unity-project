using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject rotateTarget;
    public int spinSpeed = 10;
    public Light lightColor;
    float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        transform.RotateAround(rotateTarget.transform.position, Vector3.up, spinSpeed * Time.deltaTime);

        if(counter >= 1.0f){
            float red = Random.Range(0.0f, 1.0f);
            float green = Random.Range(0.0f, 1.0f);
            float blue = Random.Range(0.0f, 1.0f);
            lightColor.color = new Color(red, green, blue);
            counter=0;
        }
    }
}
