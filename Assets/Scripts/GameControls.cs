using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuScreen.activeSelf)
            {
                PauseButton();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseButton()
    {
        // Pause the game by setting the time scale to 0
        Time.timeScale = 0f;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        // Unpause the game by setting the time scale back to 1
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
    }

    public void QuitGame()
    {
        GameManager.lostCause = "Quit";
        SceneManager.LoadScene("GameOver");
    }

}



