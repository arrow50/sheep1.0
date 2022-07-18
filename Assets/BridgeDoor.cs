using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class BridgeDoor : MonoBehaviour
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
            if(collision.gameObject.tag == "Player" && LeaderSheep.Instance.followers.Count>5)
            {
                
                print("人满了！");
                rb.isKinematic = false;
                //Destroy(collision.gameObject);
            }
        }


}
