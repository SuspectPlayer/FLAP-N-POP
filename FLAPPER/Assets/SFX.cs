using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip sfx_flap;
    public AudioClip sfx_spin;
    public AudioClip sfx_pop;
    public AudioClip sfx_popspin1;
    public AudioClip sfx_popspin2;
    public AudioClip sfx_popspin3;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
