using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        doorAnimator.SetBool("IsOpening", true);
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnimator.SetBool("IsOpening", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
