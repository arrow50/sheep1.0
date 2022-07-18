using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableObjectManager : MonoBehaviour
{
    public Transform[] trans;
    // Start is called before the first frame update
    void Start()
    {
      trans = gameObject.GetComponentsInChildren<Transform>();
        //用transform数组获取所有activepanel下的子物体

        foreach (Transform item in trans)
        {
            if (item == gameObject.transform)
            {
                return;
            }
            item.gameObject.AddComponent<breakableObject>();
        }
    }

    
}
