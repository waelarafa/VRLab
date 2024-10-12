using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCoolerTask : TaskBehaviour
{
    [SerializeField] private Transform interactionZone;
    [SerializeField] private GameObject worldCanvas;

   

    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

    public void InteractionAction()
    {
        if (GameHUD.instance.isPaused) return;
        taskDone = true;
        TaskHandler.instance.TaskDone(this);
        CoolerManager.Instance.MoveCooler();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (taskDone) return;
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (taskDone )
        {
          
            GetComponent<Collider>().enabled = false;
        }

        if (taskDone) return;
     
    }

    private void OnTriggerExit(Collider other)
    {
        if (taskDone) return;
        
    }

    public override void TaskDone()
    {
        doneEvent.Invoke();
        worldCanvas.gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
        
    }

    public override void onStart()
    {
        throw new System.NotImplementedException();
    }
}