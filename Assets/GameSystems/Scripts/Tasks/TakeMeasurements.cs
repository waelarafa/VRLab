using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeMeasurements : TaskBehaviour
{
    [SerializeField] private Transform measurementSpot;
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject measurementCanvas;
    [SerializeField] private GameObject oceanBuoy;
    private PlayerRef playerReference;
    private bool canvasEnabled;
    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

    public void InteractionAction()
    {
        if (GameHUD.instance.isPaused) return;

        canvasEnabled = !canvasEnabled;
        measurementCanvas.SetActive(canvasEnabled);

        if (canvasEnabled)
        {
            InputHandler.instance.ShowCursorAndBlockInput();
            oceanBuoy.SetActive(true);
            taskDone = true;
        }
        else if (!canvasEnabled)
        {
            InputHandler.instance.HideCursorAndEnableInput();
            taskDone = true;
            TaskHandler.instance.TaskDone(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (taskDone) return;
        if (playerReference == null)
            if (other.TryGetComponent<PlayerRef>(out playerReference))
            {
                InputHandler.instance.EnterInteractionZone(true);
                InputHandler.instance.InteractionEvent += InteractionAction;
            }
    }
    private void OnTriggerStay(Collider other)
    {
        if (taskDone && playerReference)
        {
            playerReference = null;
            InputHandler.instance.EnterInteractionZone(false);
            InputHandler.instance.InteractionEvent -= InteractionAction;
            GetComponent<Collider>().enabled = false;
        }
        if (taskDone) return;
        if (playerReference == null)
            if (other.TryGetComponent<PlayerRef>(out playerReference))
            {
                InputHandler.instance.EnterInteractionZone(true);
                InputHandler.instance.InteractionEvent += InteractionAction;
            }
    }
    private void OnTriggerExit(Collider other)
    {
        if (taskDone) return;
        if (playerReference != null)
            if (other.TryGetComponent<PlayerRef>(out PlayerRef player))
            {
                if (player == playerReference)
                {
                    playerReference = null;
                    InputHandler.instance.EnterInteractionZone(false);
                    InputHandler.instance.InteractionEvent -= InteractionAction;
                }
            }
    }

    public override void TaskDone()
    {
        doneEvent.Invoke();
        worldCanvas.gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
        oceanBuoy.SetActive(false);
    }


   
}
