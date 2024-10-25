using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBottle : MonoBehaviour
{



    public Task requiredTask;
    public Task requiredTask2;
    public void OnTriggerEnter(Collider other )
    {
        GameTask currentTask = TaskHandler.instance.currentTask;
        if (other.CompareTag("Bottle")) { 
        if (currentTask.task == requiredTask || currentTask.task == requiredTask2)
        {

            Debug.Log("cooler got trigred");
            TaskHandler.instance.TaskDone(TaskHandler.instance.currentTask.currentBehaviour);
            //    takeWaterSample.mouveNext();



        }

        }
        
    }
   
}
