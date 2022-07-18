using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followerAnim : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    public float roll = 8f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > 0.01f)
        {
            anim.SetTrigger("Run");
        }
        
        if(rb.velocity.magnitude >roll)
        {
            anim.SetTrigger("Roll");
        }

        if(rb.velocity.magnitude < 0.01f )
        {
            anim.SetTrigger("Idle A");
        }
    }
}
