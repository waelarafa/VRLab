using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeMeasurements : TaskBehaviour
{
    
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject measurementCanvas;
    [SerializeField] private GameObject oceanBuoy;
  
    [SerializeField] private GameObject MultiparameterTable;
    [SerializeField] private GameObject MultiparmeterResetUI;
    [SerializeField] private GameObject ParemeterLocation;
    private bool MutiparameterIn=false;
    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("takeMeaserment collider trigred :" + other.gameObject.name);
        if (other.CompareTag("Multiparameter") && !MutiparameterIn)
        {
            Debug.Log("Multiparameter collider trigred :");
            // oceanBuoy.transform.position = savedPosition;
            // oceanBuoy.SetActive(false);
            MutiparameterIn = true;
            measurementCanvas.SetActive(true);
            MultiparmeterResetUI.SetActive(true);
            ParemeterLocation.SetActive(true);
        }
    }
    public void MultiparameterBack()
    {
        measurementCanvas.SetActive(false);
        MultiparmeterResetUI.SetActive(false);


    }
  public void mouveNext()
    {
        TaskHandler.instance.TaskDone(this);
    }

    public override void TaskDone()
    {
        doneEvent.Invoke();
        ParemeterLocation.SetActive(false);
        worldCanvas.gameObject.SetActive(false);
        measurementCanvas.SetActive(false);
        gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
        
    }

    public override void onStart()
    {
        
    }
}
