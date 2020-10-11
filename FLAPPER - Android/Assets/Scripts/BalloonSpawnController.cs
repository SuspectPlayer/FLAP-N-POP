using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnController : MonoBehaviour
{
    public GameObject balloon;
    public float minHeight;
    public float maxHeight;
    
    public void SpawnBalloon()
    {
        Instantiate(balloon, new Vector3 (gameObject.transform.position.x, GenerateRandomHeight(), gameObject.transform.position.z), gameObject.transform.rotation);
    }

    private float GenerateRandomHeight()
    {
        float _x = Random.Range(minHeight, maxHeight);
        return _x;   
    }
}
