using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TriggerConfig
{
    public TaskTrigger trigger;                // Reference to the TaskTrigger
    public string taskNameToComplete;          // Task to complete when triggered
    public string tag;                         // Tag to compare when trigger is entered
    public GameObject[] objectsToManipulate;   // The objects that will be manipulated
}

public class TaskManager2 : MonoBehaviour
{
    public GameObject taskPrefab;              // Task prefab for UI
    public Transform taskPanel;                // UI panel to display tasks
    public TaskList taskList;                  // Reference to the ScriptableObject task list
    public Color taskTextColor;                // Color for task text in UI
    public AudioSource audioSource;            // AudioSource to play task sounds

    private int currentTaskIndex = 1;          // Tracks current task being worked on

    [Header("Trigger Configurations")]
    public TriggerConfig[] triggerConfigs;      // Array of triggers and their settings

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
        // Determine which task to complete based on the trigger
        foreach (var config in triggerConfigs)
        {
            if (config.trigger == trigger)
            {
                // Handle the task completion logic based on the task name
                HandleTaskCompletionLogic(config.taskNameToComplete, config.objectsToManipulate);

                Debug.Log("Completed task through trigger: " + config.taskNameToComplete);
                return;
            }
        }

        Debug.LogWarning("No handler for trigger: " + trigger);
    }

    // Switch statement to handle different task completion logic
    private void HandleTaskCompletionLogic(string taskName, GameObject[] objectsToManipulate)
    {
        // Manipulate objects based on the task before completing
        foreach (var obj in objectsToManipulate)
        {
            ManipulateObject(obj);
        }

        switch (taskName)
        {
            case "Task1":
                CompleteTask1();
                break;

            case "Task2":
                CompleteTask2();
                break;

            case "Task3":
                CompleteTask3();
                break;

            // Add more cases for additional tasks as needed

            default:
                Debug.LogWarning("No specific logic defined for task: " + taskName);
                CompleteCurrentTask();  // Complete the current task by default
                break;
        }
    }

    // Manipulate the specified GameObject (custom logic can be added here)
    private void ManipulateObject(GameObject obj)
    {
        if (obj != null)
        {
            // Example manipulation: Change position, scale, or perform other actions
            obj.transform.position += Vector3.up; // Move the object up for demonstration
            Debug.Log("Manipulated object: " + obj.name);
        }
        else
        {
            Debug.LogWarning("Object to manipulate is null.");
        }
    }

    // Custom logic for Task1 completion
    private void CompleteTask1()
    {
        // Task1-specific logic here
        Debug.Log("Handling completion for Task1");
        CompleteCurrentTask();
    }

    // Custom logic for Task2 completion
    private void CompleteTask2()
    {
        // Task2-specific logic here
        Debug.Log("Handling completion for Task2");
        CompleteCurrentTask();
    }

    // Custom logic for Task3 completion
    private void CompleteTask3()
    {
        // Task3-specific logic here
        Debug.Log("Handling completion for Task3");
        CompleteCurrentTask();
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

                        // Play the task completion sound
                        PlayTaskSound(taskList.tasks[currentTaskIndex].taskSound);

                        currentTaskIndex++;  // Move to the next task
                    }
                    return;
                }
            }

            Debug.LogError("Task not found: " + taskName);
        }
    }

    // Play the task's associated sound
    private void PlayTaskSound(AudioClip taskSound)
    {
        if (audioSource != null && taskSound != null)
        {
            audioSource.clip = taskSound;
            audioSource.Play();
        }
    }

    // Check if the taskName matches the current task in the sequence
    public bool IsCurrentTask(string taskName)
    {
        return currentTaskIndex < taskList.tasks.Length && taskList.tasks[currentTaskIndex].taskName == taskName;
    }
}
