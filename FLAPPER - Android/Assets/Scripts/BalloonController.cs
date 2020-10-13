using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("Balloon Components")]  
    [SerializeField] private GameObject popEffect;
    [SerializeField] private GameObject spinPopEffect;
    public bool isImpulsing;

    [Header("Balloon Move Speed")]
    private float balloonMovementSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private Rigidbody rb;
    private PlayerController playerController;
    private GameController gameController;
    private BalloonSpawnController ballonSpawner;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();
        ballonSpawner = FindObjectOfType<BalloonSpawnController>();

        balloonMovementSpeed = Random.Range(minSpeed, maxSpeed);
    }
    
    private void FixedUpdate()
    {
          transform.Translate(-Vector3.forward * balloonMovementSpeed * Time.deltaTime);
    }

    public void ImpulseForward(float _f)
    {
        StartCoroutine(ImpulseForwardRoutine(_f));
    }

    IEnumerator ImpulseForwardRoutine(float ballonIF)
    {
        isImpulsing = true;
        rb.AddForce(-Vector3.forward * ballonIF, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        isImpulsing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 here = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z + 1);

        if (collision.gameObject.tag == "Player")
        {
            if(playerController.isDashing)
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
            if (!playerController.isSuperSiyan)
            {
                gameController.GameOver();
            }
            else
            {
                ballonSpawner.SpawnBalloon();
                gameController.ResetBirdToDefault();
            }
            Destroy(gameObject);
        }
    }
}
