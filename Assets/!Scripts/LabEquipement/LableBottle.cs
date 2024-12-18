using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LableBottle : MonoBehaviour
{
    [SerializeField] private GameObject labelingUI;
   public void showLabelingUi()
    {
        labelingUI.SetActive(true);
    }
}
