using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Score Settings")]
    public float currentScore;
    public float points;

    public float currentMultiplier;
    public float multiplier;

    public TextMeshProUGUI scoreText;
    #endregion
    public void Update()
    {
        currentMultiplier = multiplier;
    }

    public void FixedUpdate()
    {
        scoreText.SetText("" + currentScore);
    }

    #region Player Score Selection
    
    public void AddPoints()
    {
        currentScore += points;
        //reset multiplier
        multiplier = 1;
    }

    public void AddMultiplier()
    {
        currentScore += points * currentMultiplier;
        //increase multiplier
        multiplier += 1;
    }
    #endregion
}
