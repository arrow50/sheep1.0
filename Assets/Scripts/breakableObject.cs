using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(MeshCollider))]

public class breakableObject : MonoBehaviour
{   
    private Rigidbody rb;
    private MeshCollider col;
    
    void Start()
    {
   
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        col = GetComponent<MeshCollider>();
        col.convex = true;
    }

   
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Player" ||collision.gameObject.tag == "Sheep")
            {
                
                print("被羊chuang了");
                rb.isKinematic = false;
                //Destroy(collision.gameObject);
            }

            Audiomanager.PlayfenceAudio();   


        }



}

