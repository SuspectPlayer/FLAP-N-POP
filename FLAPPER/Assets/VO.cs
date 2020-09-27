using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VO : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip vo_gameOver;
    public AudioClip vo_getReady;
    public AudioClip vo_3;
    public AudioClip vo_2;
    public AudioClip vo_1;
    public AudioClip vo_go;
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

    
   

    public GameController gameController;
    public float mp;
    public void Start()
    {
        audiosource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        
    }
    public void Update()
    {
        mp = gameController.currentMultiplier;
    }
    
    public void PlayVoice()
    {
        StartCoroutine(PlayVoiceLate());
    }
    
    IEnumerator PlayVoiceLate()
    {
        yield return new WaitForSeconds(1);
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
}
