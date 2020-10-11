using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour
{
    public GameObject target;
    public GameObject origin;
    public float speed;

    
    private void Start()
    {
        gameObject.transform.position = origin.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 there = target.transform.position;
        transform.Translate(there);
    }

    IEnumerator RestartTrail()
    {
        yield return new WaitForSeconds(1);
        gameObject.transform.position = origin.transform.position;
    }
}
