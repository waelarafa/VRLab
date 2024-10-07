using UnityEngine;

public abstract class TaskBehaviour : MonoBehaviour
{
    [HideInInspector] public GameTask gameTask;
    public bool taskDone;

    public void ConfigureGameTask(GameTask gameTask)
    {
        this.gameTask = gameTask;
    }
    public abstract void TaskDone();
}
