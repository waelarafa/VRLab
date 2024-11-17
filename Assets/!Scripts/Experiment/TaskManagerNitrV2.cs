using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
[System.Serializable]
public class ChildsTriggerConfigNitrV2
{
            

    public float waitTime = 1f;                    // Time to wait before completing the task
    public string childTaskName;
    public string tag;

    // Objects to activate before or after task completion
    public GameObject[] preObjectsToActivate;      // Objects to activate before completing the task
    public GameObject[] postObjectsToActivate;     // Objects to activate after completing the task

    // Objects to deactivate before or after task completion
    public GameObject[] preObjectsToDesactivate;    // Objects to deactivate before completing the task
    public GameObject[] postObjectsToDesactivate;   // Objects to deactivate after completing the task
    public GameObject[] objectsToManipulate;         // Objects to manipulate (for future use or other tasks)
    public bool usePreCompletionLogic = false; // Whether to use pre-completion logic
    public bool usePostCompletionLogic = false; // Whether to use post-completion logic
   
}

[System.Serializable]
public class TriggerConfigNitrV2
{
    public TaskTriggerNitrV2 trigger;                // Reference to the TaskTrigger
    public string taskNameToComplete;              // Task to complete when triggered
                       
      
    public float waitTime = 1f;                    // Time to wait before completing the task



    public int NombreOfChilds = 1;  // Number of completions required to complete the task
    public int currentCompletions = 0;  // Counter for completions
    public ChildsTriggerConfigNitrV2[] childsTriggerConfigNitrV2;
  
}


public class TaskManagerNitrV2 : MonoBehaviour
{
    public GameObject taskPrefab;              // Task prefab for UI
    public Transform taskPanel;                // UI panel to display tasks
    public TaskListV2 taskList;                  // Reference to the ScriptableObject task list
    public Color taskTextColor;                // Color for task text in UI
    public AudioSource audioSource;            // AudioSource to play task sounds
    public GameObject canvas;
    public GameObject Endcanvas;
    private int currentTaskIndex = 1;          // Tracks current task being worked on

    [Header("Trigger Configurations")]
    public TriggerConfigNitrV2[] triggerConfigs;      // Array of triggers and their settings

    private void Start()
    {
        InitializeTasksUI();
        InitializeTasks();     // Initialize tasks on the UI
        InitializeTriggers();  // Initialize trigger settings
        
    }

    void InitializeTasksUI()
    {
        canvas.SetActive(true);
    }
    // Initializes the tasks in the task list at the start
    void InitializeTasks()
    {
        for (int i = 0; i < taskList.tasks.Length; i++)
        {
            bool isChecked = (i == 0);  // The first task is checked (active) by default
            if (taskList.tasks[i].IsShowenInUI)
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
                config.trigger.SetTag(config.childsTriggerConfigNitrV2[0].tag);  // Assign tag to trigger


            }
        }
    }

    // Method that handles the task completion when the trigger is activated
    public void HandleTaskTrigger(TaskTriggerNitrV2 trigger)
    {
        foreach (var configs in triggerConfigs)
        {
            foreach (var config in configs.childsTriggerConfigNitrV2)
            {
                if (configs.trigger == trigger)
                {
                    if (IsCurrentTask(configs.taskNameToComplete))
                    {
                        // Get the next child task and check if it exists
                        int ChildIndex = trigger.GetChildTaskIndex();
                        if (ChildIndex != null)
                        {
                            
                            // Update the task name to the next child task
                            // config.taskNameToComplete = childTaskName;
                            // trigger.currentCompletions++;

                            // Check if required completions are met for this child task
                            if (configs.currentCompletions < configs.NombreOfChilds)
                            {
                               
                                    Debug.Log("hello");
                                    StartCoroutine(CompleteChildTaskAfterDelay(configs.childsTriggerConfigNitrV2[ChildIndex]));
                                    // trigger.DisableTrigger();  // Disable further triggering
                                    trigger.NextChildTask();
                                    trigger.SetTag(configs.childsTriggerConfigNitrV2[ChildIndex+1].tag);
                                    configs.currentCompletions++;
                               
                            }
                            else if (configs.currentCompletions == configs.NombreOfChilds)
                            {
                                StartCoroutine(CompleteTaskAfterDelay(configs));
                                trigger.DisaBleTrigger();

                            }
                            return;
                        }
                    }
                }
            }
            }

        Debug.LogWarning("No handler for trigger: " + trigger);
    }

    // Coroutine to handle task completion logic after a delay
    private IEnumerator CompleteTaskAfterDelay(TriggerConfigNitrV2 triggerConfig)
    {
        // Wait for the specified time before completing the task
        yield return new WaitForSeconds(triggerConfig.waitTime);

        // Mark the task as complete
        CompleteCurrentTask();

     
      

    }
    private IEnumerator CompleteChildTaskAfterDelay(ChildsTriggerConfigNitrV2 triggerConfig)
    {
        // Run pre-custom logic if assigned
        if (triggerConfig.usePreCompletionLogic)
        {
            ChildPreCompletionLogic(triggerConfig); // Execute custom logic before task completion
        }

        // Deactivate pre-objects if assigned
        if (triggerConfig.preObjectsToDesactivate != null && triggerConfig.preObjectsToDesactivate.Length > 0)
        {
            foreach (var obj in triggerConfig.preObjectsToDesactivate)
            {
                if (obj != null)
                    obj.SetActive(false);  // Deactivate pre objects
            }
        }

        // Activate pre-objects if assigned
        if (triggerConfig.preObjectsToActivate != null && triggerConfig.preObjectsToActivate.Length > 0)
        {
            foreach (var obj in triggerConfig.preObjectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(true);  // Activate pre objects
            }
        }

        // Wait for the specified time before completing the task
        yield return new WaitForSeconds(triggerConfig.waitTime);
        Debug.Log( " child task is completed.");
        // Mark the task as complete
        // CompleteCurrentTask();

        // Deactivate post-objects if assigned
        if (triggerConfig.postObjectsToDesactivate != null && triggerConfig.postObjectsToDesactivate.Length > 0)
        {
            foreach (var obj in triggerConfig.postObjectsToDesactivate)
            {
                if (obj != null)
                    obj.SetActive(false);  // Deactivate post objects
            }
        }

        // Activate post-objects if assigned
        if (triggerConfig.postObjectsToActivate != null && triggerConfig.postObjectsToActivate.Length > 0)
        {
            foreach (var obj in triggerConfig.postObjectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(true);  // Activate post objects
            }
        }

        // Run post-custom logic if assigned
        // Post-completion logic here
        if (triggerConfig.usePostCompletionLogic)
        {
          //  PostCompletionLogic(triggerConfig);
        }



    }

    private void PreCompletionLogic(TriggerConfigNitrV2 triggerConfig)
    {
        switch (triggerConfig.taskNameToComplete)
        {
            

      
                
            // Add more cases for additional tasks as needed

            default:
                Debug.LogWarning("No specific pre-completion logic defined for task: " + triggerConfig.taskNameToComplete);
                break;
        }
    }
    private void ChildPreCompletionLogic(ChildsTriggerConfigNitrV2 triggerConfig)
    {
        switch (triggerConfig.childTaskName)
        {
            

      
                
            // Add more cases for additional tasks as needed

            default:
                Debug.LogWarning("No specific pre-completion logic defined for task: " );
                break;
        }
    }
   
    private void PostCompletionLogic(TriggerConfigNitrV2 triggerConfig)
    {
        switch (triggerConfig.taskNameToComplete)
        {
           

            default:
                Debug.LogWarning("No specific post-completion logic defined for task: " + triggerConfig.taskNameToComplete);
                break;
        }
    }
    
   

    // Complete the current task and move to the next one
    public void CompleteCurrentTask()
    {
        if (currentTaskIndex < taskList.tasks.Length)
        {
            string taskName = taskList.tasks[currentTaskIndex].taskName;
            if (taskList.tasks[currentTaskIndex].IsShowenInUI)
            {
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
            }
            else
            {
                Debug.Log(taskName + " is completed.");
                currentTaskIndex++;
                return;
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
