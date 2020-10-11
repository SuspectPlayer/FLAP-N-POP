
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Player On Fire")]
    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject lvl3;
    public GameObject lvl4;

    [Header("Score Settings")]
    public int currentScore;
    public int highScore;
    public int currentMultiplier;

    [Header("UI Text Components")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI styleText;
    public TextMeshProUGUI mainText;
    public GameObject pauseBtn;
    public float FadeRate;
    public Image image;

    [Header("Audio Source")]
    public AudioSource audioSource;
    private VO vo;

    private float targetAlpha;
    #endregion

    public bool isGameOver;
    private UIComponentManager uiComponentManager;

    private void Start()
    {
        vo = FindObjectOfType<VO>();
        uiComponentManager = FindObjectOfType<UIComponentManager>();
        image.gameObject.SetActive(true);
        highScore = PlayerPrefs.GetInt("HighScore");
        targetAlpha = image.color.a;
        isGameOver = false;   
        currentMultiplier = 1;
        Time.timeScale = 1f;

        UpdateScoreText();
        UpdateHighScoreText();
        GameStart();
        FadeOut();

    }
    

    private void Update()
    {
        Color curColor = image.color;
        float alphaDiff = Mathf.Abs(curColor.a - targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
        }
    }

    public void UpdatePlayerEffects()
    {
        if(currentMultiplier < 2)
        {
            lvl1.SetActive(false);
            lvl2.SetActive(false);
            lvl3.SetActive(false);
            lvl4.SetActive(false);  
        }
        if(currentMultiplier == 2)
        {
            lvl1.SetActive(true);
            lvl2.SetActive(false);
            lvl3.SetActive(false);
            lvl4.SetActive(false);
        }
        if(currentMultiplier == 10)
        {
            lvl1.SetActive(false);
            lvl2.SetActive(true);
            lvl3.SetActive(false);
            lvl4.SetActive(false);
        }
        if (currentMultiplier == 20)
        {
            lvl1.SetActive(false);
            lvl2.SetActive(false);
            lvl3.SetActive(true);
            lvl4.SetActive(false);

        }
        if (currentMultiplier == 30)
        {
            lvl1.SetActive(false);
            lvl2.SetActive(false);
            lvl3.SetActive(false);
            lvl4.SetActive(true);
        }
    }

    public void FadeOut()
    {
        targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        targetAlpha = 1.0f;
    }

    #region Player Scoring
    public void AddPoints()
    {
        //increase score by regular points
        currentScore += 1;
        currentMultiplier = 1;
        UpdateScoreText();
    }

    public void AddMultiplier()
    {
        //increase score by multiplier points
        currentMultiplier += 1;
        currentScore += 1 * currentMultiplier;
        UpdateScoreText();

        multiplierText.SetText("x " + currentMultiplier);
        //play the voice over clip associated with the multiplier from VO.cs
        vo.PlayMultiplierVoice();
        //Display multiplier text to Player for 1 second      
        StartCoroutine(DisplayMultiplier());
        FindObjectOfType<PlayerController>().UpdateMultiplier(currentMultiplier);
        UpdatePlayerEffects();
    }

    public void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString() ;
    }
    public void UpdateHighScoreText()
    {
        highscoreText.text = highScore.ToString();
    }
    #endregion

    #region Helpers
    IEnumerator DisplayMultiplier()
    {
        multiplierText.gameObject.SetActive(true);
        styleText.gameObject.SetActive(true);
        mainText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        multiplierText.gameObject.SetActive(false);
        styleText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
    }
    #endregion
    
    #region Game Start
    public void GameStart()
    {
         StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        FindObjectOfType<PlayerController>().PausePlayerMovement();
        mainText.gameObject.SetActive(true);
        styleText.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);

        vo.audiosource.PlayOneShot(vo.vo_getReady, 1f);
        mainText.SetText("Get Ready!");
        yield return new WaitForSeconds(3);
        vo.audiosource.PlayOneShot(vo.vo_3, 1f);
        mainText.SetText("3");
        yield return new WaitForSeconds(1);
        vo.audiosource.PlayOneShot(vo.vo_2, 1f);
        mainText.SetText("2");
        yield return new WaitForSeconds(1);
        vo.audiosource.PlayOneShot(vo.vo_1, 1f);
        mainText.SetText("1");
        yield return new WaitForSeconds(1);
        vo.audiosource.PlayOneShot(vo.vo_go, 1f);
        mainText.SetText("GO!");
        yield return new WaitForSeconds(1);
        mainText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);
        pauseBtn.SetActive(true);
        FindObjectOfType<PlayerController>().ResumePlayerMovement();
    }
    #endregion

    #region Game Over
    public void GameOver()
    {
        vo.audiosource.PlayOneShot(vo.vo_gameOver, 1f);
        isGameOver = true;
        FindObjectOfType<PlayerController>().isPaused = true;
        if(currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            UpdateHighScoreText();
        }
            StartCoroutine(GameOverMenu());
        
    }

    IEnumerator GameOverMenu()
    {
        yield return new WaitForSeconds(2);
        uiComponentManager.EnableGameOverUI();
    }
    #endregion
}

