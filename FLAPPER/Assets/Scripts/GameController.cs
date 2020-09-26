﻿using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Player Components")]
    public GameObject player;
    public Rigidbody rb;

    [Header("Player On Fire")]
    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject lvl3;
    public GameObject lvl4;

    [Header("Score Settings")]
    public float currentScore;
    public float points;
    public float currentMultiplier;
    public float multiplier;
    [Header("UI Text Components")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI styleText;
    public TextMeshProUGUI mainText;
    public float FadeRate;
    public Image image;
    private float targetAlpha;
    #endregion

    public bool Playing;
    public AudioClip backgroundMusic;
    
    private void Start()
    {
        image.gameObject.SetActive(true);
        #region Background Music
        AudioManager.Instance.PlayLoop(backgroundMusic, player.transform);
        #endregion

        #region Fade Script 1/3

        targetAlpha = image.color.a;

        #endregion

        Playing = false;
        currentMultiplier = multiplier;
        rb.useGravity = false;
        
        GameStart();
        FadeOut();
    }
    #region UPDATE SETTINGS
    

    public void Update()
    {
        #region Fade Script 2/3
        Color curColor = image.color;
        float alphaDiff = Mathf.Abs(curColor.a - targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
        }
        #endregion

        scoreText.SetText("" + currentScore);
        currentMultiplier = multiplier;
    }
    #endregion

    public void FixedUpdate()
    {
        
        if(currentMultiplier < 2)
        {
            lvl1.SetActive(false);
            lvl2.SetActive(false);
            lvl3.SetActive(false);
            lvl4.SetActive(false);
            
        }
        if(currentMultiplier > 1)
        {
            lvl1.SetActive(true);
            
        }
        if(currentMultiplier > 2)
        {
            lvl2.SetActive(true);
            lvl1.SetActive(false);
           
        }
        if(currentMultiplier > 3)
        {
            lvl3.SetActive(true);
            lvl2.SetActive(false);
            
        }
        if(currentMultiplier > 4)
        {
            lvl4.SetActive(true);
            lvl3.SetActive(false);
        }
    }
    #region Fade Script 3/3
    public void FadeOut()
    {
        targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        targetAlpha = 1.0f;
    }
    #endregion

    #region Player Scoring


    public void AddPoints()
    {
        //increase score by regular points
        currentScore += points;

        //reset multiplier
        multiplier = 1;
    }

    public void AddMultiplier()
    {
        //increase score by multiplier points
        currentScore += points * currentMultiplier;

        //increase multiplier
        multiplier += 1;
        multiplierText.SetText("x " + multiplier);

        //Display multiplier text to Player for 1 second      
        StartCoroutine(DisplayMultiplier());
    }
    #endregion

    #region Helpers
    IEnumerator DisplayMultiplier()
    {
        multiplierText.gameObject.SetActive(true);
        styleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        multiplierText.gameObject.SetActive(false);
        styleText.gameObject.SetActive(false);
    }
    #endregion

    #region Game Start
    public void GameStart()
    {
            StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        mainText.gameObject.SetActive(true);
        mainText.SetText("Get Ready!");
        yield return new WaitForSeconds(3);
        mainText.SetText("3");
        yield return new WaitForSeconds(1);
        mainText.SetText("2");
        yield return new WaitForSeconds(1);
        mainText.SetText("1");
        yield return new WaitForSeconds(1);
        mainText.SetText("GO!");
        yield return new WaitForSeconds(1);
        mainText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        Playing = true;
        rb.useGravity = true;
    }
    #endregion

    #region Game Over
    public void GameOver()
    {
        mainText.gameObject.SetActive(true);
        mainText.SetText("GAME OVER!");
        Playing = false;
        rb.useGravity = false;
    }
    #endregion
}