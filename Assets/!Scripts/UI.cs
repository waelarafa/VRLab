using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject endPanel;

    public void ShowEndPanel()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(true);  
    }


    public void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

}
