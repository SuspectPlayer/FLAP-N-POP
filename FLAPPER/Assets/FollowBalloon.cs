using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBalloon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject balloon = GameObject.FindGameObjectWithTag("Balloon");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            GameObject balloon = GameObject.FindGameObjectWithTag("Balloon");
            transform.position = balloon.transform.position;
        }
    }
}
