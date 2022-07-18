using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LeaderSheep : MonoBehaviour
{
    public static LeaderSheep Instance;

    
    public List<GameObject> followers = new List<GameObject>();

    private void Awake()
			{
				Instance = this;
			}




    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Sheep" && !followers.Contains(other.gameObject)){
            
            followers.Add(other.gameObject);
            other.gameObject.GetComponent<FollowTargetAndGather>().SetLeader(this.gameObject);
        }
    }




}
