using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseText;
    public GameObject resumeUI;
    public GameObject retryUI;

    public GameController gameController;
    
    CursorLockMode desiredMode;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        desiredMode = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameController.Playing)
            {
                if (gameIsPaused)
                    Resume();
                else
                    PauseGame();
            }
            
        }
    }

    public void Resume()
    {
        retryUI.SetActive(false);
        resumeUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.visible = false;
        desiredMode = CursorLockMode.Confined;
    }

    void PauseGame()
    {
        retryUI.SetActive(false);
        resumeUI.SetActive(true);
        pauseMenuUI.SetActive(true);
        pauseText.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
        desiredMode = CursorLockMode.None;
    }
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    public void GameOverUI()
    {
        retryUI.SetActive(true);
        resumeUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        pauseText.SetActive(false);
        Cursor.visible = true;
        desiredMode = CursorLockMode.None;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Quit()
    {
        Debug.LogWarning("Game quits if not in Unity editor");
        Application.Quit();
    }
}