using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TriggerConfig
{
    public TaskTrigger trigger;         // Reference to the TaskTrigger
    public string taskNameToComplete;   // Task to complete when triggered
    public string tag;                  // Tag to compare when trigger is entered
}

public class TaskManager2 : MonoBehaviour
{
    public GameObject taskPrefab;        // Task prefab for UI
    public Transform taskPanel;          // UI panel to display tasks
    public TaskList taskList;            // Reference to the ScriptableObject task list
    public Color taskTextColor;          // Color for task text in UI

    private int currentTaskIndex = 1;    // Tracks current task being worked on

    [Header("Trigger Configurations")]
    public TriggerConfig[] triggerConfigs; // Array of triggers and their settings

    private void Start()
    {
        InitializeTasks();     // Initialize tasks on the UI
        InitializeTriggers();  // Initialize trigger settings
    }

    // Initializes the tasks in the task list at the start
    void InitializeTasks()
    {
        for (int i = 0; i < taskList.tasks.Length; i++)
        {
            bool isChecked = (i == 0);  // The first task is checked (active) by default
            CreateTask(taskList.tasks[i].taskName, taskList.tasks[i].taskDescription, isChecked);
        }
    }

    // Create a task item in the UI
    void CreateTask(string taskName, string taskDescription, bool isChecked)
    {
        GameObject taskObj = Instantiate(taskPrefab, taskPanel);  // Instantiate a new task UI
        taskObj.name = taskName;  // Set the name for taskObj

        TaskItem taskItem = taskObj.GetComponent<TaskItem>();
        taskItem.SetTaskDescription(taskDescription);  // Set task description
        taskItem.SetTaskTextColor(taskTextColor);      // Set custom task text color

        // Set initial toggle state
        taskItem.toggle.isOn = isChecked;  // First task is checked by default
        taskItem.toggle.interactable = false;  // Disable user interaction with toggle
    }

    // Initialize trigger configurations and set their task names and tags
    private void InitializeTriggers()
    {
        foreach (var config in triggerConfigs)
        {
            if (config.trigger != null)
            {
                config.trigger.taskNameToComplete = config.taskNameToComplete;  // Assign task to trigger
                config.trigger.SetTag(config.tag);  // Assign tag to trigger
            }
        }
    }

    // Method that handles the task completion when the trigger is activated
    public void HandleTaskTrigger(TaskTrigger trigger)
    {
        CompleteCurrentTask();  // Complete the current task in the sequence
        Debug.Log("Task completed through trigger: " + trigger.taskNameToComplete);
    }

    // Complete the current task and move to the next one
    public void CompleteCurrentTask()
    {
        if (currentTaskIndex < taskList.tasks.Length)
        {
            string taskName = taskList.tasks[currentTaskIndex].taskName;

            foreach (Transform child in taskPanel)
            {
                if (child.gameObject.name == taskName)
                {
                    TaskItem taskItem = child.GetComponent<TaskItem>();
                    if (taskItem != null)
                    {
                        taskItem.CompleteTask();  // Mark task as complete
                        Debug.Log(taskName + " is completed.");
                        currentTaskIndex++;  // Move to the next task
                    }
                    return;
                }
            }

            Debug.LogError("Task not found: " + taskName);
        }
    }


    // Update the next task in the sequence and mark it as active
    //void UpdateNextTask()
    //{
    //    if (currentTaskIndex < taskList.tasks.Length)
    //    {
    //        string nextTaskName = taskList.tasks[currentTaskIndex].taskName;

    //        foreach (Transform child in taskPanel)
    //        {
    //            if (child.gameObject.name == nextTaskName)
    //            {
    //                TaskItem taskItem = child.GetComponent<TaskItem>();
    //                if (taskItem != null)
    //                {
    //                    taskItem.toggle.isOn = true;  // Mark next task as active
    //                    Debug.Log("Next task activated: " + nextTaskName);
    //                }
    //                return;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("All tasks completed!");
    //    }
    //}

    // Check if the taskName matches the current task in the sequence
    public bool IsCurrentTask(string taskName)
    {
        return currentTaskIndex < taskList.tasks.Length && taskList.tasks[currentTaskIndex].taskName == taskName;
    }
}
