using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        }
        if(Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        }
        if(Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0,-speed * Time.deltaTime));
        }
         if(Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 0,speed * Time.deltaTime));
        }

        float RotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 2f;
        float RotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * 2f;
        transform.localEulerAngles = new Vector3(RotationY, RotationX, 0f);
    }
}
