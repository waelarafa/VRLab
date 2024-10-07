using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public int tasks;
    public int taskFinished;
    [SerializeField] private GameObject finalCanvas;

    public InputHandler inputHandler;

    
    public void End()
    {
        if (taskFinished >= tasks)
        {
            finalCanvas.SetActive(true);
            inputHandler.LockUnlockCursor(true);
        }
         
    }
    public void GoToScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
