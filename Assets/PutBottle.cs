using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBottle : MonoBehaviour
{

    TakeWaterSample takeWaterSample;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {

            takeWaterSample.mouveNext();

        }
        
    }
   
}
