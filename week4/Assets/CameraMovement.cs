using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float moveSpeed = 5f;
    //Define the speed at which the object moves.

    private Vector3 currentEulerAngles;
    private float rotationSpeed = 90;
    //kecepatan rotasi

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float updownR = 0;
        float leftrightR = 0;

        float frontBackMovement = 0;
        float leftRightMovement = 0;

        //camera input
        //up down left right
        if (Input.GetKey(KeyCode.UpArrow)) { updownR -= 1; }
        if (Input.GetKey(KeyCode.DownArrow)) { updownR += 1; }
        if (Input.GetKey(KeyCode.LeftArrow)) { leftrightR -= 1; }
        if (Input.GetKey(KeyCode.RightArrow)) { leftrightR += 1; }

        currentEulerAngles += new Vector3(updownR, leftrightR, 0) * Time.deltaTime * rotationSpeed;
        transform.eulerAngles = currentEulerAngles;

        //movement input
        //front back left right 
        if (Input.GetKey(KeyCode.W)) { frontBackMovement += 1; }
        if (Input.GetKey(KeyCode.S)) { frontBackMovement -= 1; }
        if (Input.GetKey(KeyCode.A)) { leftRightMovement -= 1; }
        if (Input.GetKey(KeyCode.D)) { leftRightMovement += 1; }

        //update the position
        transform.Translate(new Vector3(leftRightMovement * moveSpeed * Time.deltaTime, 0, frontBackMovement * moveSpeed * Time.deltaTime));
    }
}
