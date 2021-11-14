using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotation = rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(rotation, 0, 0);
    }
    void OnCollisionEnter(Collision col)
     {
         if(col.gameObject.tag == "Destroyable")
         {
             Destroy(col.gameObject);
         }
     }
}
