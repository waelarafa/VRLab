using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeMeasurements : TaskBehaviour
{
    
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject measurementCanvas;
    [SerializeField] private GameObject oceanBuoy;
    [SerializeField] private GameObject AreaCanvas;
    [SerializeField] private GameObject MultiparameterTable;
    [SerializeField] private GameObject TakeBackMultiparameter;
    private Vector3 savedPosition;
    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("takeMeaserment collider trigred :" + other.gameObject.name);
        if (other.CompareTag("Multiparameter"))
        {
           // oceanBuoy.transform.position = savedPosition;
           // oceanBuoy.SetActive(false);
            measurementCanvas.SetActive(true);
            

        }
    }
    public void MultiparameterBack()
    {
        measurementCanvas.SetActive(false);
        

    }
  public void mouveNext()
    {
        TaskHandler.instance.TaskDone(this);
    }

    public override void TaskDone()
    {
        doneEvent.Invoke();
        worldCanvas.gameObject.SetActive(false);
        measurementCanvas.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
        
    }

    public override void onStart()
    {
        savedPosition = transform.position;
    }
}
