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
    public GameObject bottleTable;
    [SerializeField] private int MaxNbOfRinzing;
    private int nbOfRinzing = 0;
    private bool BottleIn=false;
    public UnityEvent startEvent;
    public UnityEvent doneEvent;
  

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BottleRinzing collider trigred :" + other.tag);
        if (other.CompareTag("Bottle") && !BottleIn)
        {
            Debug.Log("Bottle collider trigred :");
            BottleIn = true;
         
            //other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(nbOfRinzing+1).gameObject.SetActive(true);
            nbOfRinzing++;
            if(nbOfRinzing== MaxNbOfRinzing)
            {
                bottleTable.SetActive(true);
            }



        }
        if (other.CompareTag("Drop") && BottleIn)
        {
            BottleIn = false;
        }

    }
    public override void TaskDone()
    {
         doneEvent.Invoke();
        // worldCanvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
    }

    public override void onStart()
    {
        throw new System.NotImplementedException();
    }
}
