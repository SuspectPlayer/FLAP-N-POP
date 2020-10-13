using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Player On Fire")]
    [SerializeField] private GameObject lvl1;
    [SerializeField] private GameObject lvl2;
    [SerializeField] private GameObject lvl3;
    [SerializeField] private GameObject lvl4;

    [Header("Score Settings")]
    public int currentScore;
    public int highScore;
    public int currentMultiplier;

    [Header("UI Text Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI styleText;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private GameObject pauseBtn;

    [Header("Audio Source")]
    [SerializeField] public AudioSource audioSource;
    private VO vo;

    public bool isGameOver;
    private UIComponentManager uiComponentManager;
    #endregion

    #region Initialization
    private void Start()
    {
        vo = FindObjectOfType<VO>();
        uiComponentManager = FindObjectOfType<UIComponentManager>();
        highScore = PlayerPrefs.GetInt("HighScore");
        isGameOver = false;   
        currentMultiplier = 1;
        Time.timeScale = 1f;

        UpdateScoreText();
        UpdateHighScoreText();
        GameStart();
    }
	#endregion

	#region Player Trail Effect
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
    #endregion

    #region Player Scoring
    public void AddPoints()
    {
        //increase score by regular points
        currentScore += 1;
        currentMultiplier = 1;
        ResetBirdToDefault();
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
        vo.PlayMultiplierVoice(currentMultiplier);
        //Display multiplier text to Player for 1 second      
        StartCoroutine(DisplayMultiplier());
        FindObjectOfType<PlayerController>().UpdatePlayerMesh(currentMultiplier);
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

    public void ResetBirdToDefault()
    {
        currentMultiplier = 1;
        UpdatePlayerEffects();
        FindObjectOfType<PlayerController>().UpdatePlayerMesh(currentMultiplier);
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

