using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCoolerTask : TaskBehaviour
{
    [SerializeField] private Transform interactionZone;
    [SerializeField] private GameObject worldCanvas;

    private PlayerRef playerReference;

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
        InputHandler.instance.EnterInteractionZone(false);
        InputHandler.instance.InteractionEvent -= InteractionAction;
    }
}