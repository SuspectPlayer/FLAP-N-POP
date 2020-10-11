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

    [Header("Movement Settings")]
    public float flapStrength;

    [Header("Switches")]
    public bool spinning;
    public bool isPaused;
    private GameController gameController;

    public GameObject mesh1;
    public GameObject mesh2;
    public GameObject mesh3;
    public GameObject mesh4;

    [Header("Audio Source")]
    public SFX sfx;

    #endregion
    private void Awake()
    {
        mesh1.SetActive(true);
        gameController = FindObjectOfType<GameController>();
        isPaused = true;
        rb = player.GetComponent<Rigidbody>();
        StartCoroutine(StartDelay());
        sfx = FindObjectOfType<SFX>();
    }

    #region Player Inputs
    private void Update()
    {
        if (!isPaused)
        {
            //handles buttons for flap and spin commands
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Flap();
                sfx.audioSource.PlayOneShot(sfx.sfx_flap,1f);
                //reset fall velocity
            }

            if(transform.position.y <= 20)
            {
                isPaused = true;
                gameController.GameOver();
            }

            if (transform.position.y >= 100)
            {
                isPaused = true;
                gameController.GameOver();
            }    
        }

    }

    public void SwipeLeft()
    {
        if (!isPaused)
        {
            SpinLeft();
        }
    }

    public void SwipeRight()
    {
        if (!isPaused)
        {
            SpinRight();
        }
    }

    #endregion
    #region Player Abilities
    public void Flap()
    {
        //applys rigidbody force upwards on player if isn't spinning and game is currently in progress
        if(spinning == false)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * flapStrength * 100);
        }        
    }
    public void SpinLeft()
    {
        //play spinning animation for 1 second
        spinning = true;
        anim.SetTrigger("SpinLeft"); 
        StartCoroutine(SpinDelay());
    }
    public void SpinRight()
    {
        //play spinning animation for 1 second
        spinning = true;
        anim.SetTrigger("SpinRight");
        StartCoroutine(SpinDelay());
    }
    #endregion
    #region Helpers
    IEnumerator SpinDelay()
    {
        yield return new WaitForSeconds(0.7f);
        spinning = false;        
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(7);
        isPaused = false;
    }

    public void PausePlayerMovement()
    {
        isPaused = true;
        rb.useGravity = false;
    }

    public void ResumePlayerMovement()
    {
        isPaused = false;
        rb.useGravity = true;
    }

    public void UpdateMultiplier(int _multiplier)
    {
        if (_multiplier < 5)
        {
            mesh1.SetActive(true);
            mesh2.SetActive(false);
            mesh3.SetActive(false);
            mesh4.SetActive(false);
        }
        else if (_multiplier >= 5 && _multiplier < 10)
        {
            mesh1.SetActive(false);
            mesh2.SetActive(true);
            mesh3.SetActive(false);
            mesh4.SetActive(false);
        }
        else if (_multiplier >= 10 && _multiplier < 20)
        {
            mesh1.SetActive(false);
            mesh2.SetActive(false);
            mesh3.SetActive(true);
            mesh4.SetActive(false);
        }
        else if (_multiplier >= 20)
        {
            mesh1.SetActive(false);
            mesh2.SetActive(false);
            mesh3.SetActive(false);
            mesh4.SetActive(true);
        }
    }
    #endregion
    #region Collisions
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Balloon")
        {
            gameController = FindObjectOfType<GameController>();
            BalloonSpawnController ballonSpawner = FindObjectOfType<BalloonSpawnController>();
            if (spinning)
             {
                gameController.AddMultiplier();
                ballonSpawner.SpawnBalloon();
                if (gameController.currentMultiplier < 2)
                {
                    sfx.audioSource.PlayOneShot(sfx.sfx_popspin1, 1f);
                }
                if (gameController.currentMultiplier > 1)
                {
                    sfx.audioSource.PlayOneShot(sfx.sfx_popspin2, 1f);
                }
                if (gameController.currentMultiplier > 2)
                {
                    sfx.audioSource.PlayOneShot(sfx.sfx_popspin3, 1f);
                }
                    
             }
             else
             {
                gameController.AddPoints();
                ballonSpawner.SpawnBalloon();
                if (gameController.currentMultiplier > 1)
                 {
                     sfx.audioSource.PlayOneShot(sfx.sfx_pop, 1f);
                 }
                 if (gameController.currentMultiplier < 2)
                 {
                     sfx.audioSource.PlayOneShot(sfx.sfx_pop, 1f);
                 }
             }
        }   
    }
    #endregion  
}
