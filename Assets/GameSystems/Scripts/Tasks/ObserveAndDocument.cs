using UnityEngine;
using UnityEngine.Events;

public class ObserveAndDocument : TaskBehaviour
{
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject observeAndDocumentCanvas;

    
    private bool canvasEnabled;
    public Progress progress;
    public int progressIndex;
    public int toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

    public void docSaved()
    {
        TaskHandler.instance.TaskDone(this);
    }

    public override void TaskDone()
    {

       
        doneEvent.Invoke();
        worldCanvas.gameObject.SetActive(false);
        observeAndDocumentCanvas.SetActive(false);
      //  gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
     
    }

    public override void onStart()
    {
        observeAndDocumentCanvas.SetActive(true);
    }
}
