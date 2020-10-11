using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("Balloon Components")]  
    public GameObject popEffect;
    public GameObject spinPopEffect;

    [Header("Balloon Move Speed")]
    private float balloonMovementSpeed;
    public float minSpeed;
    public float maxSpeed;

    private PlayerController playerController;
    private GameController gameController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();

        balloonMovementSpeed = Random.Range(minSpeed, maxSpeed);
    }
    
    private void Update()
    {
        transform.Translate(-Vector3.forward * balloonMovementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 here = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z + 1);

        if (collision.gameObject.tag == "Player")
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

        if(collision.gameObject.tag == "DeadZone")
        {
            gameController.GameOver();
            Destroy(gameObject);
        }
    }
}
