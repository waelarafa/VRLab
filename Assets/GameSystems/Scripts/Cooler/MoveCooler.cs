using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCooler : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CoolerManager.Instance.MoveCooler();
            }
        }
    }
}
