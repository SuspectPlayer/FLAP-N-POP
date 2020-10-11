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

    [Header("Intialising Components")]
    public AudioSource audiosource;
    public GameController gameController;

    public void Start()
    {
        audiosource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
    }

    //public class to be called from other scripts to play voice over
    public void PlayMultiplierVoice()
    {
        StartCoroutine(PlayVoiceLate());
    }

    //plays Voice Over delayed by 1 second when player is getting multipliers
    IEnumerator PlayVoiceLate()
    {
        yield return new WaitForSeconds(0.7f);
        audiosource.PlayOneShot(GenerateRandomMultiplierVoiceOver(), 1f);
    }

    private AudioClip GenerateRandomMultiplierVoiceOver()
    {
        int ranNo = Random.Range(1, 107);
        AudioClip _clip = null;
        if(ranNo < 30)
        {
            _clip = vo_v1;
        }
        else if(ranNo >= 30 && ranNo < 40)
        {
            _clip = vo_v2;
        }
        else if(ranNo >= 40 && ranNo < 50)
        {
            _clip = vo_v3;
        }
        else if (ranNo >= 50 && ranNo < 55)
        {
            _clip = vo_v4;
        }
        else if (ranNo >= 55 && ranNo < 60)
        {
            _clip = vo_v5;
        }
        else if (ranNo >= 60 && ranNo < 65)
        {
            _clip = vo_v6;
        }
        else if (ranNo >= 65 && ranNo < 70)
        {
            _clip = vo_v7;
        }
        else if (ranNo >= 70 && ranNo < 80)
        {
            _clip = vo_v8;
        }
        else if (ranNo >= 80 && ranNo < 85)
        {
            _clip = vo_v9;
        }
        else if (ranNo >= 85 && ranNo < 87)
        {
            _clip = vo_v10;
        }
        else if (ranNo >= 87 && ranNo < 93)
        {
            _clip = vo_v11;
        }
        else if (ranNo >= 93 && ranNo < 95)
        {
            _clip = vo_v12;
        }
        else if (ranNo >= 95 && ranNo < 97)
        {
            _clip = vo_v13;
        }
        else if (ranNo >= 97 && ranNo < 99)
        {
            _clip = vo_v14;
        }
        else if (ranNo >= 99 && ranNo < 101)
        {
            _clip = vo_v15;
        }
        else if (ranNo >= 101 && ranNo < 103)
        {
            _clip = vo_v16;
        }
        else if (ranNo >= 103 && ranNo < 105)
        {
            _clip = vo_v17;
        }
        else if (ranNo >= 105 && ranNo < 107)
        {
            _clip = vo_v18;
        }
        return _clip;
    }
}