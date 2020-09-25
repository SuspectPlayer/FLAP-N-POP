using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnController : MonoBehaviour
{
    public GameObject balloon;
    
    public void SpawnBalloon()
    {

        Instantiate(balloon, gameObject.transform.position, gameObject.transform.rotation);
    }
}
