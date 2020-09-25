using ChrisTutorials.Persistent;
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
    public GameObject armature;
    public Rigidbody rb;
    public Animator anim;
    public GameObject manager;
    public GameObject balloonSpawner;
   
    public AudioClip flapClip;
    public AudioClip spinClip;

    public AudioClip popClip;
    public AudioClip spinClip1;
    public AudioClip spinClip2;
    public AudioClip spinClip3;
    public AudioClip spinFailClip;

    [Header("Player Input Controls")]
    public KeyCode flap;
    public KeyCode spin;

    [Header("Movement Settings")]
    public float flapStrength;
    public float fallSpeed;
    public float fallVelocity;


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
                //reset fall velocity
                fallVelocity = 0;
            }

            if (Input.GetKeyDown(spin))
            {
                Spin();
                //reset fall velocity for comfort
                fallVelocity = 0;
            }
            //increase fall velocity when nothing is pressed

            fallVelocity = fallVelocity += fallSpeed;
            transform.Translate(Vector3.down * fallVelocity * Time.deltaTime);

            //rotate the bird down when decending ---> NOT WORKING
            //if (fallVelocity > .01f)
            //{
            //    if (armature.transform.eulerAngles.x < 90f)
            //    {
            //        float ninetyDegrees = -90;
            //        Vector3 newVector = new Vector3(ninetyDegrees, 180, 0);

            //        armature.transform.eulerAngles = Vector3.Lerp(armature.transform.rotation.eulerAngles, newVector, 4 * Time.deltaTime);
            //    }

            //    currentEulerAngles = transform.rotation.eulerAngles;
            //    currentRotation.eulerAngles = currentEulerAngles;
            //    currentEulerAngles = currentRotation.eulerAngles;

            //    if (fallVelocity < .2f)
            //    {
            //        float zeroDegrees = 0;
            //        Vector3 origVector = new Vector3(zeroDegrees, 180, 0);

            //        armature.transform.eulerAngles = Vector3.Lerp(armature.transform.rotation.eulerAngles, origVector, 10 * Time.deltaTime);
            
        
           
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

            AudioManager.Instance.Play(flapClip, this.transform);
        }        
    }
    public void Spin()
    {
        //play spinning animation for 1 second
        spinning = true;
        anim.SetBool("Fly", false);
        anim.SetBool("Spin", true);
        rb.AddForce(Vector3.up * flapStrength);
        AudioManager.Instance.Play(spinClip, this.transform);
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
                    if (manager.GetComponent<GameController>().currentMultiplier < 1)
                    {
                        AudioManager.Instance.Play(popClip, this.transform);
                    }
                    if (manager.GetComponent<GameController>().currentMultiplier > 0)
                    {
                        AudioManager.Instance.Play(spinClip1, this.transform);
                    }
                    if (manager.GetComponent<GameController>().currentMultiplier > 1)
                    {
                        AudioManager.Instance.Play(spinClip2, this.transform);
                    }
                    if (manager.GetComponent<GameController>().currentMultiplier > 2)
                    {
                        AudioManager.Instance.Play(spinClip3, this.transform);
                    }
                }
                else
                {
                    manager.GetComponent<GameController>().AddPoints();
                    balloonSpawner.GetComponent<BalloonSpawnController>().SpawnBalloon();
                    if (manager.GetComponent<GameController>().currentMultiplier > 1)
                    {
                        AudioManager.Instance.Play(spinFailClip, this.transform);

                    }
                    if (manager.GetComponent<GameController>().currentMultiplier < 2)
                    {
                        AudioManager.Instance.Play(popClip, this.transform);

                    }

                }
                break;
        }
        
        
    }
    #endregion

    
}
