using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Voice over class for playing all the games commentry
public class VO : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Starting Voice Over")]
    public AudioClip vo_gameOver;
    public AudioClip vo_getReady;
    public AudioClip vo_3;
    public AudioClip vo_2;
    public AudioClip vo_1;
    public AudioClip vo_go;
    [Header("Multiplier Voice Overs")]
    public AudioClip vo_v1;
    public AudioClip vo_v2;
    public AudioClip vo_v3;
    public AudioClip vo_v4;
    public AudioClip vo_v5;
    public AudioClip vo_v6;
    public AudioClip vo_v7;
    public AudioClip vo_v8;
    public AudioClip vo_v9;
    public AudioClip vo_v10;
    public AudioClip vo_v11;
    public AudioClip vo_v12;
    public AudioClip vo_v13;
    public AudioClip vo_v14;
    public AudioClip vo_v15;
    public AudioClip vo_v16;
    public AudioClip vo_v17;
    public AudioClip vo_v18;
    #endregion

    #region Initialising Components
    [Header("Intialising Components")]
    public AudioSource audiosource;
    public GameController gameController;
    public float mp; //currentmultiplier from GameController script

    public void Start()
    {
        audiosource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        
    }
    public void Update()
    {
        mp = gameController.currentMultiplier;
    }
    #endregion

    #region Voice Over Selector
    //public class to be called from other scripts to play voice over
    public void PlayVoice()
    {
        StartCoroutine(PlayVoiceLate());
    }
    
    //plays Voice Over delayed by 1 second when player is getting multipliers
    IEnumerator PlayVoiceLate()
    {
        yield return new WaitForSeconds(1);
        //following if statements, plays voice over audio clip files based on currentmultiplier
        if (mp == 2)
        {
            audiosource.PlayOneShot(vo_v1, 1f);
        }

        if (mp == 5)
        {
            audiosource.PlayOneShot(vo_v2, 1f);
        }
        if (mp == 10)
        {
            audiosource.PlayOneShot(vo_v3, 1f);
        }
        if (mp == 15)
        {
            audiosource.PlayOneShot(vo_v4, 1f);
        }
        if (mp == 20)
        {
            audiosource.PlayOneShot(vo_v5, 1f);
        }
        if (mp == 25)
        {
            audiosource.PlayOneShot(vo_v6, 1f);
        }
        if (mp == 30)
        {
            audiosource.PlayOneShot(vo_v7, 1f);
        }
        if (mp == 35)
        {
            audiosource.PlayOneShot(vo_v8, 1f);
        }
        if (mp == 40)
        {
            audiosource.PlayOneShot(vo_v9, 1f);
        }
        if (mp == 45)
        {
            audiosource.PlayOneShot(vo_v10, 1f);
        }
        if (mp == 50)
        {
            audiosource.PlayOneShot(vo_v11, 1f);
        }
        if (mp == 55)
        {
            audiosource.PlayOneShot(vo_v12, 1f);
        }
        if (mp == 60)
        {
            audiosource.PlayOneShot(vo_v13, 1f);
        }
        if (mp == 65)
        {
            audiosource.PlayOneShot(vo_v14, 1f);
        }
        if (mp == 70)
        {
            audiosource.PlayOneShot(vo_v15, 1f);
        }
        if (mp == 75)
        {
            audiosource.PlayOneShot(vo_v16, 1f);
        }
        if (mp == 80)
        {
            audiosource.PlayOneShot(vo_v17, 1f);
        }
    }
    #endregion
}
