using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    public Text label;
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float jumpheight = 1.6f;
    
    int counter = 0;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void FixedUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        translation *= Time.fixedDeltaTime;
        transform.Translate(0, 0, translation);

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        rotation *= Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);

        if (translation != 0)
        {
            if (!anim.GetBool("isJumping"))
            {
                if (translation > 0 && speed < 8f)
                {
                    speed += 0.05f;
                }
                anim.SetBool("isWalking", true);
                anim.SetFloat("characterSpeed", translation);
            }
        }
        else
        {
            speed = 5f;
            anim.SetBool("isWalking", false);
            anim.SetFloat("characterSpeed", 0);
        }

        if (Input.GetKey(KeyCode.Space) && !anim.GetBool("isJumping"))
        {
            Vector3 newVelocity = new Vector3(rb.velocity.x, jumpheight, rb.velocity.z);
            rb.velocity = newVelocity;

            anim.SetBool("isJumping", true);
        }
        if (anim.GetBool("isJumping"))
        {
            speed -= 0.001f;
            translation *= Time.fixedDeltaTime;
            anim.SetFloat("characterSpeed", translation);
        }

        if(anim.GetFloat("characterSpeed") > 0.12f){
            anim.SetBool("isRunning", true);
        }else{
            anim.SetBool("isRunning", false);
        }
        Debug.Log(rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destroyable")
        {
            counter++;
            label.text = counter.ToString();
            Destroy(col.gameObject);
        }
        anim.SetBool("isJumping", false);
    }
}
