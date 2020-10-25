using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public SceneFader sceneFader;
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;
    public GameObject pauseButton;

    public void PauseGame()
    {
        pauseMenuUi.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUi.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void BackToMainMenu()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
