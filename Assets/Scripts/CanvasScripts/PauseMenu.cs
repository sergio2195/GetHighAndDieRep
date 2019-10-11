using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

   [SerializeField]
   private GameObject pauseMenuUI, controlsUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Escape")) {
            if(GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadControls()
    {
        Debug.Log("Loading Controls...");
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        LevelChangerScript.Instance.FadeToNextLevel("Menu");
    }

    public void BackButton()
    {
        pauseMenuUI.SetActive(true);
        controlsUI.SetActive(false);
    }

}
