using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audiomanager : MonoBehaviour
{
    // Start is called before the first frame update
    static Audiomanager current;
    
    [Header("环境声音")]
    public AudioClip ambientClip;
    public AudioClip musicClip;

     [Header("羊叫")]
    public AudioClip[] sheepClips;

    [Header("动物")]
    public AudioClip[] cowClips;
    public AudioClip[] donkeyClips;    

    [Header("碰撞")]
    public AudioClip[] bushClips;
    public AudioClip[] fenceClips;
    public AudioClip scarecrowClip; 

    [Header("河流")]
    public AudioClip riverClip;


    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource sheepSource;
    AudioSource cowSource;
    AudioSource donkeySource;
    AudioSource fenceSource;
    AudioSource bushSource;
    AudioSource scarecrowSource;
    AudioSource riverSource;




    
    private void Awake()
    {
        current = this;

        DontDestroyOnLoad(gameObject);
        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        sheepSource = gameObject.AddComponent<AudioSource>();
        cowSource = gameObject.AddComponent<AudioSource>();
        donkeySource = gameObject.AddComponent<AudioSource>();
        fenceSource = gameObject.AddComponent<AudioSource>();
        bushSource = gameObject.AddComponent<AudioSource>();
        scarecrowSource = gameObject.AddComponent<AudioSource>();
        riverSource = gameObject.AddComponent<AudioSource>();

        StartLevelAudio();

    }

    void StartLevelAudio()
    {
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }

    public static void PlaybushAudio()
    {
        int index = Random.Range(0, current.bushClips.Length);
        current.bushSource.clip = current.bushClips[index];
        current.bushSource.volume = current.bushSource.volume/2;
        current.bushSource.Play();
    }

    public static void PlayfenceAudio()
    {
        int index = Random.Range(0, current.fenceClips.Length);
        current.fenceSource.clip = current.fenceClips[index];
        current.fenceSource.volume = current.fenceSource.volume/2;
        current.fenceSource.Play();
        current.fenceSource.volume = current.fenceSource.volume*2;
    }

    public static void PlaysheepAudio()
    {
        int index = Random.Range(0, current.sheepClips.Length);
        current.sheepSource.clip = current.sheepClips[index];
        current.sheepSource.Play();
        if (Input.GetKey(KeyCode.Space))
        {
            Audiomanager.PlaysheepAudio(); 
        
        }
    }








}
