using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScenes : MonoBehaviour
{
  
    public void GoToScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
