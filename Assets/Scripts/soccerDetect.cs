using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soccerDetect : MonoBehaviour
{
    public GameObject pondSheepRed;
    public GameObject pondSheepBlue;
    private int life =2;


    public  ParticleSystem particle; 
    void Start()
    {
        pondSheepRed.SetActive(false);
        pondSheepBlue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
//
        }
    }

    void OnTriggerEnter(Collider collision)
    
        {
            if(collision.gameObject.tag == "BallBlue")
            {
                
                print("进蓝球了！");
                particle.Play();
                //StartCoroutine("Delay");//开始协程
                pondSheepBlue.SetActive(true);
                life-=1;


                if(collision.gameObject.tag == "BallRed")
            {
                
                print("进红球了！");
                particle.Play();
                //StartCoroutine("Delay");//开始协程
                pondSheepRed.SetActive(true);
                life-=1;
            }
                              
        }

  

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);//延时0.5秒再继续向下执行
    }

}
}
