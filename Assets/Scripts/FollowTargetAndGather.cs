using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowTargetAndGather : MonoBehaviour
{
    public float leaderWeight = 1f;
    public float commonDirWeight = 0.1f;
    public float commonAttractWeight = 1f;
    public float leaderAttactWeight = 2f;
    public float commonRebelWeight = 1f;
    GameObject leader;
    List<GameObject> allSheeps = new List<GameObject>();
    public float deTectRadius = 30;
    public float attractRadius = 15;
    public float rebelRadius = 5;
    
    Vector3 leaderDir;
    Vector3 leaderPos;
    Vector3 averageDir;
    Vector3 averagePos;
    int count = 0;
    void Update()
    {
        averageDir = Vector3.zero;
        averagePos = Vector3.zero;
        count = 0;
        if(leader ==null)
        {return;}
        
        leaderDir = leader.transform.forward;
        leaderPos = leader.transform.position;

        //get all near sheeps
        foreach(GameObject sheep in allSheeps){
            if ((sheep.transform.position - this.transform.position).magnitude < deTectRadius){
                if (sheep != this.gameObject){
                        //朝向align叠加
                    averageDir += sheep.transform.forward;
                    averagePos += sheep.transform.position;
                    count+=1;
                }
            }
        }

        if (count > 0 ){
            averageDir /= count;
            averagePos /= count;
            
            //adujust
            this.transform.forward += ((averageDir - this.transform.forward).normalized * commonDirWeight + (leaderDir - this.transform.forward).normalized * leaderWeight);

            //attract
            Vector3 relativePos = averagePos - this.transform.position;
            if (relativePos.magnitude > attractRadius){
                this.GetComponent<Rigidbody>().AddForce(relativePos.normalized * commonAttractWeight);
            }
            if ((leaderPos - this.transform.position).magnitude > attractRadius){
                this.GetComponent<Rigidbody>().AddForce((leaderPos - this.transform.position).normalized * leaderAttactWeight);
            }
            //rebel
            if (relativePos.magnitude < rebelRadius){
                this.GetComponent<Rigidbody>().AddForce(-relativePos.normalized * commonRebelWeight);
            }
        }
    }
    public void SetLeader(GameObject leaderSheep){
        leader = leaderSheep;
        allSheeps.Add(leader);
        foreach(GameObject sheep in leader.GetComponent<LeaderSheep>().followers){
            allSheeps.Add(sheep);
        }
    }
}
