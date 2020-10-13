using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIComponentManager : MonoBehaviour
{
    private bool gameIsPaused;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private GameObject stylishText;
    [SerializeField] private GameObject multiplierText;
    [SerializeField] private GameObject textBox;

    private void Start()
    {
        gameIsPaused = false;
    }

    public void ResumeTheGame()
    {
        if (gameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            gameOverMenuUI.SetActive(false);
            stylishText.SetActive(false);
            multiplierText.SetActive(false);
            textBox.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            FindObjectOfType<PlayerController>().ResumePlayerMovement();
        }
    }

    public void EnablePauseUI()
    {
        GameController gameController = FindObjectOfType<GameController>();
        if (!gameIsPaused && !gameController.isGameOver)
        {
            pauseMenuUI.SetActive(true);
            gameOverMenuUI.SetActive(false);
            stylishText.SetActive(false);
            multiplierText.SetActive(false);
            textBox.SetActive(false);
            Time.timeScale = 0f;
            gameIsPaused = true;
            FindObjectOfType<PlayerController>().PausePlayerMovement();
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void EnableGameOverUI()
    {
        pauseMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(true);
        stylishText.SetActive(false);
        multiplierText.SetActive(false);
        textBox.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}