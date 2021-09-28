using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate : MonoBehaviour
{
    public GameObject target;
    public int speed;

    public Light buttonLight;
    public float objectTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectTime += Time.deltaTime;
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);

        if(objectTime >= 1.0f)
        {
            var r = Random.Range(0.0f, 1.0f);
            var g = Random.Range(0.0f, 1.0f);
            var b = Random.Range(0.0f, 1.0f);

            buttonLight.color = new Color(r, g, b);
            objectTime = 0.0f;
        }
    }


}
