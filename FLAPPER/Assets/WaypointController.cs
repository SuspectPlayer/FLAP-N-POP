using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public GameObject balloon;
    void Update()
    {
        GameObject balloon = GameObject.FindGameObjectWithTag("Balloon");
        transform.LookAt(balloon.transform.position);     
    }
}
