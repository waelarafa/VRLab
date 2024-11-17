using UnityEngine;

[CreateAssetMenu(fileName = "NewTaskListV2", menuName = "Task ListV2")]
public class TaskListV2 : ScriptableObject
{
    [System.Serializable]
    public class childTask
    {
        public string taskName;           // The name of the task
        [TextArea]
        public string taskDescription;    // The description of the task
          // Array to hold child tasks
    }
    [System.Serializable]
    public class TaskV2
    {
        public string taskName;           // The name of the task
        [TextArea]
        public string taskDescription;    // The description of the task
        public AudioClip taskSound;       // The sound to play when the task is completed
        public bool IsShowenInUI = true;  // Flag to show tasks in the Task Panel

        // Add child tasks array
        public childTask[] childTasks;         // Array to hold child tasks
    }

    public TaskV2[] tasks; // Array to hold top-level tasks
}
