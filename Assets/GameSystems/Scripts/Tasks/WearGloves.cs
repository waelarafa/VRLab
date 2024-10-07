using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WearGloves : TaskBehaviour
{
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject areasCanvas;
    [SerializeField] private GameObject gloves;

    private PlayerRef playerReference;
    public Progress progress;
    public int progressIndex;
    public int toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

    public void InteractionAction()
    {
            taskDone = true;
            TaskHandler.instance.TaskDone(this);       
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
        areasCanvas.gameObject.SetActive(true);
        gloves.gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
    }
}
