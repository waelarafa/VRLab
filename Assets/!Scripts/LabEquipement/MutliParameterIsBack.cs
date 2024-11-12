using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutliParameterIsBack : MonoBehaviour
{
  
    public GameObject MeasurementBack;
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Multiparameter")){
            MeasurementBack.SetActive(false);
            TaskHandler.instance.TaskDone(TaskHandler.instance.currentTask.currentBehaviour);


        }
    }

}
