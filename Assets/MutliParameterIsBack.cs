using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutliParameterIsBack : MonoBehaviour
{
    TakeMeasurements Measerment;
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Multiparameter")){
            Measerment.mouveNext();


        }
    }

}
