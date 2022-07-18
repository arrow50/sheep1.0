using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotate : MonoBehaviour
{
    public float rotateSpeed =25f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.Q))

        {

            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0, Space.World);

        }      

        if (Input.GetKey(KeyCode.E))

        {

            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0,Space.World);

    }
}
}
