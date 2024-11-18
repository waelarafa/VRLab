using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleIsBack : MonoBehaviour
{
    public GameObject bottleTable;
    
    // Start is called before the first frame update
public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {
            bottleTable.SetActive(false);
            TaskHandler.instance.TaskDone(TaskHandler.instance.currentTask.currentBehaviour);
        }
    }
}
