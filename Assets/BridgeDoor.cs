using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDoor : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    public GameObject doorLock;
    
    void Start()
    {
   
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        col = GetComponent<Collider>();
        //col.convex = true;
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
                doorLock.GetComponent<Rigidbody>().isKinematic = false;
            }
        }


}
