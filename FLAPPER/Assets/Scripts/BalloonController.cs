
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Balloon Components")]
    public GameObject gameManager;
    public GameObject popEffect;
    public GameObject spinPopEffect;
    [Header("Balloon Move Speed")]
    private float balloonMovementSpeed;
    public float minSpeed;
    public float maxSpeed;
   
    [Header("Random Height Speed")]
    public float minRange;
    public float maxRange;
    private float randomSpeed;

    private PlayerController playerController;
    #endregion

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        balloonMovementSpeed = Random.Range(minSpeed, maxSpeed);
        randomSpeed = Random.Range(minRange, maxRange);
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        
    }
    private void Update()
    {
        transform.Translate(-Vector3.forward * balloonMovementSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * randomSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        
        Vector3 here = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z + 1);

        if (collision.gameObject.CompareTag("Player"))
        {
            if(playerController.spinning)
            {
                Instantiate(spinPopEffect, here, Quaternion.Euler(0, 0, 0));
                Destroy(gameObject);
            }
            else
            {
                Instantiate(popEffect, here, Quaternion.Euler(0, 0, 0));
                Destroy(gameObject);
            }
        }

        else
        {
            gameManager.GetComponent<GameController>().GameOver();
            Destroy(gameObject);
        }
        
    }
}
