using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG
{
    public class HandsSelect : MonoBehaviour
{
    public int HandModelId = 1;

    public HandModelSelector hms;

    void Start()
    {
        if (hms == null)
        {
            hms = FindObjectOfType<HandModelSelector>();
        }

        if (hms == null)
        {
            Debug.Log("No Hand Model Selector Found in Scene. Will not be able to switch hand models");
        }
            StartCoroutine(MyCoroutine());
       
    }
        IEnumerator MyCoroutine()
        {
            Debug.Log("Coroutine started");

            // Wait for 2 seconds
            yield return new WaitForSeconds(1);

            hms.ChangeHandsModel(HandModelId, false);
        }
       
  
}




    }




 
