using UnityEngine;

[CreateAssetMenu(fileName = "NewTaskList", menuName = "Task List")]
public class TaskList : ScriptableObject
{
    [System.Serializable]
    public class Task
    {
        public string taskName; // The name of the task
        [TextArea]
        public string taskDescription; // The description of the task
    }

    public Task[] tasks; // Array to hold multiple tasks
}
