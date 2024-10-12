using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCoolerTask : TaskBehaviour
{
   
 
    [SerializeField] private GameObject AreaObjects;
    [SerializeField] private Transform NextAreaPosition;

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
        AreaObjects.transform.position = NextAreaPosition.position;
    }
}