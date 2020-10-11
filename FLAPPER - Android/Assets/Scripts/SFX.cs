using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mono Class for Sound Effects
public class SFX : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public AudioClip sfx_flap;
    public AudioClip sfx_spin;
    public AudioClip sfx_pop;
    public AudioClip sfx_popspin1;
    public AudioClip sfx_popspin2;
    public AudioClip sfx_popspin3;
    #endregion

    #region Initialising Components
    public AudioSource audioSource;
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    #endregion
}