using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class TakeWaterSample : TaskBehaviour
{
    [SerializeField] private GameObject worldCanvas;
    [SerializeField] private GameObject waterBottle;
    [SerializeField] private float waterBottleAnimationDelay;
    private PlayerRef playerReference;
    [SerializeField] private GameObject label;
    [SerializeField] private WaterSampleBox box;
    public Progress progress;
    public int progressIndex;
    public float toAdd;

    public UnityEvent startEvent;
    public UnityEvent doneEvent;

  
    public void InteractionAction()
    {
        StartCoroutine(TaskCoroutine());
    }
    IEnumerator TaskCoroutine()
    {
        startEvent.Invoke();
        waterBottle.SetActive(true);
        taskDone = true;
        yield return new WaitForSeconds(waterBottleAnimationDelay);
        label.SetActive(true);
        box.AddWaterBottle(waterBottle.transform.GetChild(0).gameObject);

        CoolerManager.Instance.AddBottle();
       // TaskHandler.instance.TaskDone(this);
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
    }

    public void TaskDoneFinal()
    {
        TaskHandler.instance.TaskDone(this);
        if (taskDone && playerReference)
        {
            playerReference = null;
            InputHandler.instance.EnterInteractionZone(false);
            InputHandler.instance.InteractionEvent -= InteractionAction;
        }
        worldCanvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
        progress.toAdd = toAdd;
        progress.AddToProgress(progressIndex);
    }
}
