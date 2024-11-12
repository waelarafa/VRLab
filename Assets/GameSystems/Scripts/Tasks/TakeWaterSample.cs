using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class TakeWaterSample : TaskBehaviour
{
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject BottleLable;




    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

  
 // TaskHandler.instance.TaskDone(this);
 
    private void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Bottle"))
    {
            BottleLable.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(true);  
            other.transform.GetChild(1).gameObject.SetActive(true);


        }
       


    }
    public void mouveNext()
    {
        TaskHandler.instance.TaskDone(this);
    }

    public override void TaskDone()
    {
        doneEvent.Invoke();
        worldCanvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
    }

  

    public override void onStart()
    {
  
    }
}
