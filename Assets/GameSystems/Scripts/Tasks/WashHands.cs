using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WashHands : TaskBehaviour
{
    [SerializeField] private GameObject worldCanvas;

    public Progress progress;
    public int progressIndex;
    public int toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;
    public void InteractionAction()
    {
        if(!taskDone)
        {
            taskDone = true;
            TaskHandler.instance.TaskDone(this);
        }
    }
    private void OnTriggerEnter(Collider other)
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
