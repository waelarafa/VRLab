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



    
    public void End()
    {
        if (taskFinished >= tasks-1)
        {
            finalCanvas.SetActive(true);
          
        }
         
    }
    public void GoToScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
