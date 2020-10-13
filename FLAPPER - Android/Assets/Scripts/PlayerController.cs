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
    public ParticleSystem speedTrailEffect;

    [Header("Lane Settings")]
    public int currentLane;
    public float laneTransitionSpeed;
    public float laneOneX;
    public float laneTwoX;
    public float laneThreeX;

    [Header("Movement Settings")]
    public float flapStrength;
    public float dashForce;

    [Header("Switches")]
    public bool spinning;
    public bool isDashing;
    public bool isPaused;
    public bool isSuperSiyan;

    private GameController gameController;
    private BalloonSpawnController ballonSpawner;

    [Header("Super Siyan Skins")]
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
        ballonSpawner = FindObjectOfType<BalloonSpawnController>();
        isPaused = true;
        isSuperSiyan = false;
        currentLane = 2;
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

    #endregion
    #region Player Abilities
    public void Flap()
    {
        //applys rigidbody force upwards on player if isn't spinning and game is currently in progress
        if(!spinning)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * flapStrength * 100);
        }        
    }
    public void SpinLeft()
    {
        if (!isPaused && !spinning && !isDashing)
        {
            if (currentLane == 2)
            {
                spinning = true;
                StartCoroutine(SpinLeftAnim());
                StartCoroutine(SpinDelay());
                StartCoroutine(MoveToLaneOne());
            }
            else if (currentLane == 3)
            {
                spinning = true;
                StartCoroutine(SpinLeftAnim());
                StartCoroutine(SpinDelay());
                StartCoroutine(MoveToLaneTwo());
            }
        }
    }
    public void SpinRight()
    {     
        if (!isPaused && !spinning && !isDashing)
        {
            if (currentLane == 2)
            {
                spinning = true;
                StartCoroutine(SpinRightAnim());
                StartCoroutine(SpinDelay());
                StartCoroutine(MoveToLaneThree());
            }
            else if (currentLane == 1)
            {
                spinning = true;
                StartCoroutine(SpinRightAnim());
                StartCoroutine(SpinDelay());
                StartCoroutine(MoveToLaneTwo());
            }
        }
    }

    private IEnumerator SpinLeftAnim()
    {
        anim.SetBool("SpinLeft", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("SpinLeft", false);
    }

    private IEnumerator SpinRightAnim()
    {
        anim.SetBool("SpinRight", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("SpinRight", false);
    }

    private IEnumerator MoveToLaneOne()
    {
        currentLane = 1;
        Vector3 trans = transform.position;
        Vector3 newTrans = new Vector3(laneOneX, transform.position.y, transform.position.z);
        float delta = 0;
        while (transform.position.x != laneOneX)
        {
             delta += Time.deltaTime * laneTransitionSpeed;
             this.transform.position = Vector3.Lerp(trans, newTrans, delta);
             yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveToLaneTwo()
    {
        currentLane = 2;
        Vector3 trans = transform.position;
        Vector3 newTrans = new Vector3(laneTwoX, transform.position.y, transform.position.z);
        float delta = 0;
        while (transform.position.x != laneTwoX)
        {
            delta += Time.deltaTime * laneTransitionSpeed;
            this.transform.position = Vector3.Lerp(trans, newTrans, delta);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveToLaneThree()
    {
        currentLane = 3;
        Vector3 trans = transform.position;
        Vector3 newTrans = new Vector3(laneThreeX, transform.position.y, transform.position.z);
        float delta = 0;
        while (transform.position.x != laneThreeX)
        {
            delta += Time.deltaTime * laneTransitionSpeed;
            this.transform.position = Vector3.Lerp(trans, newTrans, delta);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Dash()
    {
        if (!isPaused)
        {
            isDashing = true;
            anim.SetTrigger("Dash");
            StartCoroutine(DashDelay());
            ImpulseBalloons();
            StartCoroutine(SpeedTrailDelay());
        }
    }

    IEnumerator SpeedTrailDelay()
    {
        var main = speedTrailEffect.main;
        main.simulationSpeed = 100f;
        Color color;
        color = Color.white;
        //ColorUtility.TryParseHtmlString("#00DFFF", out color);
        color.a = 0.55f;
        main.startColor = color;

        yield return new WaitForSeconds(0.8f);
        var newMain = speedTrailEffect.main;
        newMain.simulationSpeed = 20f;
        Color newColor;
        newColor = Color.white;
        newColor.a = 0.35f;
        main.startColor = newColor;
    }

    private void ImpulseBalloons()
    {
        GameObject[] allBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        foreach (GameObject r in allBalloons)
        {
            BalloonController bCon = r.GetComponent<BalloonController>();
            if (bCon != null)
            {
                bCon.ImpulseForward(dashForce);
            }
        }
    }

    #endregion

    #region Helpers
    IEnumerator SpinDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        spinning = false;        
    }

    IEnumerator DashDelay()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        isDashing = false;
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSecondsRealtime(7);
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

    public void UpdatePlayerMesh(int _multiplier)
    {
        if (_multiplier < 5)
        {
            mesh1.SetActive(true);
            mesh2.SetActive(false);
            mesh3.SetActive(false);
            mesh4.SetActive(false);
            isSuperSiyan = false;
        }
        else if (_multiplier >= 5 && _multiplier < 10)
        {
            mesh1.SetActive(false);
            mesh2.SetActive(true);
            mesh3.SetActive(false);
            mesh4.SetActive(false);
            isSuperSiyan = true;
        }
        else if (_multiplier >= 10 && _multiplier < 20)
        {
            mesh1.SetActive(false);
            mesh2.SetActive(false);
            mesh3.SetActive(true);
            mesh4.SetActive(false);
            isSuperSiyan = true;
        }
        else if (_multiplier >= 20)
        {
            mesh1.SetActive(false);
            mesh2.SetActive(false);
            mesh3.SetActive(false);
            mesh4.SetActive(true);
            isSuperSiyan = true;
        }
    }

    #endregion

    #region Collisions
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Balloon")
        {
            
            if (isDashing)
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
