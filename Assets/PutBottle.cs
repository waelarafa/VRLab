using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBottle : MonoBehaviour
{
   

    public TakeWaterSample takeWaterSample;
  
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {
            Debug.Log("cooler got trigred");
            TaskHandler.instance.TaskDone(TaskHandler.instance.currentTask.currentBehaviour);
        //    takeWaterSample.mouveNext();
      
      
            


        }
        
    }
   
}
