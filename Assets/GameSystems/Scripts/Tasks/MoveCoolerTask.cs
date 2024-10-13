using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCoolerTask : TaskBehaviour
{
   
 
    [SerializeField] private GameObject AreaObjects;
    [SerializeField] private GameObject oldObjects;


    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

  



 

    public override void TaskDone()
    {
        doneEvent.Invoke();
       
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
        
    }

    public override void onStart()
    {
        // Instead of transform.position, use MovePosition for Rigidbody objects

        AreaObjects.SetActive(true);
        oldObjects.SetActive(false);
        TaskHandler.instance.TaskDone(this);
        // Store the children


    }
}