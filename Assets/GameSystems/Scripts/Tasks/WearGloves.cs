using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WearGloves : TaskBehaviour
{
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject areasCanvas;
    [SerializeField] private GameObject gloves;

    public Progress progress;
    public int progressIndex;
    public int toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;


   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Hand"))
        {
            
          
            TaskHandler.instance.TaskDone(this);
        }
        
       
    }
    public override void TaskDone()
    {
        doneEvent.Invoke();
        worldCanvas.gameObject.SetActive(false);
        areasCanvas.gameObject.SetActive(true);
        gloves.gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
    }

    public override void onStart()
    {
        throw new System.NotImplementedException();
    }
}
