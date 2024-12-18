using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTask : MonoBehaviour
{
    // Start is called before the first frame update
  public void passTask()
    {
        TaskHandler.instance.TaskDone(TaskHandler.instance.currentTask.currentBehaviour);
    }
}
