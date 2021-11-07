using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool("Start"))
        {
            animator.SetBool("Start", false);
            animator.SetBool("Ends", false);
            animator.SetBool("Extends", true);
        }
        else
        {
            animator.SetBool("Extends", false);
            animator.SetBool("Start", true);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
