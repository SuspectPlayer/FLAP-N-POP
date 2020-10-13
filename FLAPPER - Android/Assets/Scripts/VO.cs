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

    public AudioSource audiosource;
     #endregion

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    //public class to be called from other scripts to play voice over
    public void PlayMultiplierVoice(int _x)
    {
        StartCoroutine(PlayVoiceLate(_x));
    }

    //plays Voice Over delayed by 0.7 second when player is getting multipliers
    IEnumerator PlayVoiceLate(int _multiplier)
    {
        yield return new WaitForSeconds(0.7f);
        PlayVoiceOverAccordingToMultiplier(_multiplier);
    }

    private void PlayVoiceOverAccordingToMultiplier(int _cm)
    {
        if (_cm <= 22)
        {
            if (_cm == 2)
            {
                audiosource.PlayOneShot(vo_v1, 1f);
            }
            else if (_cm == 3)
            {
                audiosource.PlayOneShot(vo_v2, 1f);
            }
            else if (_cm == 6)
            {
                audiosource.PlayOneShot(vo_v3, 1f);
            }
            else if (_cm == 8)
            {
                audiosource.PlayOneShot(vo_v4, 1f);
            }
            else if (_cm == 10)
            {
                audiosource.PlayOneShot(vo_v5, 1f);
            }
            else if (_cm == 11)
            {
                audiosource.PlayOneShot(vo_v6, 1f);
            }
            else if (_cm == 12)
            {
                audiosource.PlayOneShot(vo_v7, 1f);
            }
            else if (_cm == 13)
            {
                audiosource.PlayOneShot(vo_v8, 1f);
            }
            else if (_cm == 14)
            {
                audiosource.PlayOneShot(vo_v9, 1f);
            }
            else if (_cm == 15)
            {
                audiosource.PlayOneShot(vo_v10, 1f);
            }
            else if (_cm == 16)
            {
                audiosource.PlayOneShot(vo_v11, 1f);
            }
            else if (_cm == 17)
            {
                audiosource.PlayOneShot(vo_v12, 1f);
            }
            else if (_cm == 18)
            {
                audiosource.PlayOneShot(vo_v13, 1f);
            }
            else if (_cm == 19)
            {
                audiosource.PlayOneShot(vo_v14, 1f);
            }
            else if (_cm == 20)
            {
                audiosource.PlayOneShot(vo_v15, 1f);
            }
            else if (_cm == 21)
            {
                audiosource.PlayOneShot(vo_v16, 1f);
            }
            else if (_cm == 22)
            {
                audiosource.PlayOneShot(vo_v17, 1f);
            }
            else if (_cm == 23)
            {
                audiosource.PlayOneShot(vo_v18, 1f);
            }
        }
        else
        {
            audiosource.PlayOneShot(GenerateRandomMultiplierVoiceOver(), 1f);
        }
    }

    private AudioClip GenerateRandomMultiplierVoiceOver()
    {
        int ranNo = Random.Range(60, 107);
        AudioClip _clip = null;
        if (ranNo >= 60 && ranNo < 65)
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