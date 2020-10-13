using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    public float laneOneX;
    public float laneTwoX;
    public float laneThreeX;

    [SerializeField] private GameObject _lvl1balloon;
    [SerializeField] private GameObject _lvl2balloon;
    [SerializeField] private GameObject _lvl3balloon;
    [SerializeField] private GameObject _lvl4balloon;
    [SerializeField] private GameObject _lvl5balloon;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    
    public void SpawnBalloon()
    {
        int _mp = gameController.currentMultiplier;
        if (_mp <= 4)
        {
            Instantiate(_lvl1balloon, new Vector3(GenerateRandomLane(), GenerateRandomHeight(), gameObject.transform.position.z), gameObject.transform.rotation);
        }
        else if (_mp <= 7)
        {
            Instantiate(_lvl2balloon, new Vector3(GenerateRandomLane(), GenerateRandomHeight(), gameObject.transform.position.z), gameObject.transform.rotation);
        }
        else if (_mp <= 10)
        {
            Instantiate(_lvl3balloon, new Vector3(GenerateRandomLane(), GenerateRandomHeight(), gameObject.transform.position.z), gameObject.transform.rotation);
        }
        else if (_mp <= 14)
        {
            Instantiate(_lvl4balloon, new Vector3(GenerateRandomLane(), GenerateRandomHeight(), gameObject.transform.position.z), gameObject.transform.rotation);
        }
        else if (_mp >= 15)
        {
            Instantiate(_lvl5balloon, new Vector3(GenerateRandomLane(), GenerateRandomHeight(), gameObject.transform.position.z), gameObject.transform.rotation);
        }
    }

    private float GenerateRandomHeight()
    {
        float _x = Random.Range(minHeight, maxHeight);
        return _x;   
    }

    private float GenerateRandomLane()
    {
        float _laneValue;
        int _RanNum = Random.Range(1, 4);
        if(_RanNum == 2)
        {
            _laneValue = laneOneX;
            return _laneValue;
        }
        else if (_RanNum == 1)
        {
            _laneValue = laneTwoX;
            return _laneValue;
        }
        else
        {
            _laneValue = laneThreeX;
            return _laneValue;
        }
    }
}
