using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameHUD : IGlobalSingleton<GameHUD>
{
    [SerializeField] private GameObject pauseCanvas;

    public bool isPaused { get; private set; }
    public void PauseClicked()
    {
        isPaused = !isPaused;
        pauseCanvas.SetActive(true);
    }
    public void LeadGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        InputHandler.instance.UnlockInput();
    }
}
