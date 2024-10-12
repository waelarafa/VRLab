using UnityEngine;
public enum Task
{
    ObserveAndDocument,
    WashYourHands,
    WearGloves,
    TakeWaterSample,
    TakeMeasurements,
    TakeMoreWaterSamples,
    MoveCooler
}
public class TaskHandler : IGlobalSingleton<TaskHandler>
{
    [SerializeField] private GameTask[] gameTasks;

    public GameEnd gameEnd;
    private GameTask _currentTask;
    public GameTask currentTask => _currentTask;
    private void Start()
    {
      //  _currentTask.gameEnd= gameEnd;
        gameEnd.tasks = gameTasks.Length;
        if (gameTasks.Length != 0)
        {
            _currentTask = gameTasks[0];
            _currentTask.EnableBehaviour();
            
        }
        else
            Debug.LogError("There's no active TASKS");
    }
    public void TaskDone(TaskBehaviour task)
    {
        for (int i = 0; i < gameTasks.Length; i++)
        {
            if(task.gameTask == gameTasks[i])
            {
                _currentTask.TaskDone();
                if((i + 1) < gameTasks.Length)
                {
                    _currentTask = gameTasks[i + 1];
                    _currentTask.EnableBehaviour();
                    
                }
                gameEnd.taskFinished++;
            }
        }
    }
}
[System.Serializable]
public class GameTask
{
    [SerializeField] private Task task;
    [SerializeField] private GameObject taskListItem;
    [SerializeField] private TaskBehaviour behaviourHolder;
    public TaskBehaviour currentBehaviour => behaviourHolder;
    public GameEnd gameEnd;
    public string taskTitle;
    public void DisableTaskListItem()
    {
        taskListItem.SetActive(false);
        gameEnd.End();

    }
    public bool isDone { get; private set; }
    public void TaskDone()
    {
        DisableTaskListItem();
        isDone = true;
        behaviourHolder.TaskDone();
       // PopupHandler.instance.TaskFinished(this);
    }
    public void EnableBehaviour()
    {
        behaviourHolder?.gameObject.SetActive(true);
        behaviourHolder?.ConfigureGameTask(this);
        behaviourHolder.onStart();
    }
}