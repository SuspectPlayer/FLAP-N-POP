using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Player Components")]
    public GameObject player;
    public Rigidbody rb;
    public Animator anim;
    public GameObject manager;
    public GameObject balloonSpawner;
    public AudioSource flapClip;

    [Header("Player Input Controls")]
    public KeyCode flap;
    public KeyCode spin;

    [Header("Movement Settings")]
    public float flapStrength;

    [Header("Switches")]
    public bool spinning;
    public bool isAlive;
    #endregion
    private void Start()
    {
        isAlive = false;
        rb = player.GetComponent<Rigidbody>();
        StartCoroutine(StartDelay());
       
    }

    #region Player Inputs
    private void Update()
    {
        if (isAlive)
        {
            //handles buttons for flap and spin commands
            if (Input.GetKeyDown(flap))
            {
                Flap();
            }

            if (Input.GetKeyDown(spin))
            {
                Spin();
            }
        }
    }
    #endregion
    #region Player Abilities
    public void Flap()
    {
        //applys rigidbody force upwards on player if isn't spinning and game is currently in progress
        if(spinning == false)
        {
            rb.AddForce(Vector3.up * flapStrength);
            
            
        }        
    }
    public void Spin()
    {
        //play spinning animation for 1 second
        spinning = true;
        anim.SetBool("Fly", false);
        anim.SetBool("Spin", true);
        rb.AddForce(Vector3.up * flapStrength);
        StartCoroutine(SpinDelay());
    }
    #endregion
    #region Helpers
    IEnumerator SpinDelay()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Fly", true);
        anim.SetBool("Spin", false);
        spinning = false;
        yield return new WaitForSeconds(1);
        
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(7);
        isAlive = true;
    }
    #endregion
    #region Collisions
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Balloon":
                if (spinning)
                {
                    manager.GetComponent<GameController>().AddMultiplier();
                    balloonSpawner.GetComponent<BalloonSpawnController>().SpawnBalloon();
                }
                else
                {
                    manager.GetComponent<GameController>().AddPoints();
                    balloonSpawner.GetComponent<BalloonSpawnController>().SpawnBalloon();
                }
                break;
        }
        
    }
    #endregion
}
