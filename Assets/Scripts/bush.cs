using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class bush : MonoBehaviour
{ 
    public int life =3;
    private Animator anim;   
    private Collider col;
    public  ParticleSystem particle; 
    public GameObject alive;
    public GameObject dead;
    public GameObject bushSheep;

    public GameObject chick;
    private PlayableDirector timeline;
    



    void Start()
    {
        if(chick!=null)
        {
            timeline = chick.GetComponent<PlayableDirector>();
        }

        if(bushSheep!=null)
        {   
            bushSheep.GetComponent<FollowTargetAndGather>().enabled = false; 
            bushSheep.GetComponent<Rigidbody>().isKinematic = true; 
            bushSheep.GetComponent<Collider>().enabled = false;  
            bushSheep.GetComponent<Animator>().enabled = false;    
                 
        
        }
       
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            col.enabled = false;
            StartCoroutine("Delay");//开始协程

            alive.SetActive(false);
            dead.SetActive(true);



            if(bushSheep!=null)
            { 
                               
                bushSheep.GetComponent<FollowTargetAndGather>().enabled = true;
                bushSheep.GetComponent<Rigidbody>().isKinematic = false;
                bushSheep.GetComponent<Collider>().enabled = true;
                bushSheep.GetComponent<Animator>().enabled = true;    
                bushSheep = null;                
                
            }

            if(chick!=null)
        {
            timeline.Play();
            chick = null;
        }




        }
    }

    void OnTriggerEnter(Collider other)
        {
            if( Vector3.Distance (other.gameObject.transform.position, gameObject.transform.position)>5f)
            {return;}
            

            if(other.gameObject.tag == "Player" ||other.gameObject.tag == "Sheep")
            {
                life -= 1;
                print("被羊撞了"+life);
                anim.SetTrigger("shake"); 
                //Destroy(collision.gameObject);    
                Audiomanager.PlaybushAudio();            
            }

            
        }
    
    void PlayParticleSystem()
    {
        particle.Play();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);//延时0.5秒再继续向下执行
    }
    


}
