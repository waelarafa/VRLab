using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
[System.Serializable]
public class TriggerConfigNitrV2
{
    public TaskTriggerNitr trigger;                // Reference to the TaskTrigger
    public string taskNameToComplete;          // Task to complete when triggered
    public string tag;                         // Tag to compare when trigger is entered
    public GameObject[] objectsToManipulate;   // The objects that will be manipulated
    public float waitTime = 1f;                // Time to wait before completing the task

    // Switches to determine if pre/post completion logic should be executed
    public bool usePreCompletionLogic = false; // Whether to use pre-completion logic
    public bool usePostCompletionLogic = false; // Whether to use post-completion logic
}


public class TaskManagerNitrV2 : MonoBehaviour
{
    public GameObject taskPrefab;              // Task prefab for UI
    public Transform taskPanel;                // UI panel to display tasks
    public TaskList taskList;                  // Reference to the ScriptableObject task list
    public Color taskTextColor;                // Color for task text in UI
    public AudioSource audioSource;            // AudioSource to play task sounds
    public GameObject canvas;
    public GameObject Endcanvas;
    private int currentTaskIndex = 1;          // Tracks current task being worked on

    [Header("Trigger Configurations")]
    public TriggerConfigNitr[] triggerConfigs;      // Array of triggers and their settings

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
                config.trigger.SetTag(config.tag);  // Assign tag to trigger
            }
        }
    }

    // Method that handles the task completion when the trigger is activated
    public void HandleTaskTrigger(TaskTriggerNitrV2 trigger)
    {
        // Find the corresponding TriggerConfig
        foreach (var config in triggerConfigs)
        {
            if (config.trigger == trigger)
            {
                // Check if the task name matches the current task
                if (IsCurrentTask(config.taskNameToComplete))
                {
                    StartCoroutine(CompleteTaskAfterDelay(config));
                    return;
                }
            }
        }

        Debug.LogWarning("No handler for trigger: " + trigger);
    }

    // Coroutine to handle task completion logic after a delay
    private IEnumerator CompleteTaskAfterDelay(TriggerConfigNitr triggerConfig)
    {
        // Pre-completion logic here
        if (triggerConfig.usePreCompletionLogic)
        {
            PreCompletionLogic(triggerConfig);
        }

        // Wait for the specified time defined in TriggerConfig
        yield return new WaitForSeconds(triggerConfig.waitTime); // Use the waitTime from TriggerConfig

        // Mark the task as complete
        CompleteCurrentTask();

        // Post-completion logic here
        if (triggerConfig.usePostCompletionLogic)
        {
            PostCompletionLogic(triggerConfig);
        }

    }

    private void PreCompletionLogic(TriggerConfigNitr triggerConfig)
    {
        switch (triggerConfig.taskNameToComplete)
        {
            

            case "Task 2":
                CompleteTask2_PreLogic(triggerConfig);
                break;
            case "Task 3":
                CompleteTask3_PreLogic(triggerConfig.objectsToManipulate[0]);
                break;
            case "Task 5":
                CompleteTask5_PreLogic(triggerConfig.objectsToManipulate[0]);
                break;
            case "Task 6":
                CompleteTask6_PreLogic(triggerConfig.objectsToManipulate[0]);
                break;
            case "Task 8":
                CompleteTask8_PreLogic(triggerConfig);
                 break;
            case "Task 8.1":
                CompleteTask8_1_PreLogic(triggerConfig);
                break;
            case "Task 17":
                CompleteTask17_PreLogic(triggerConfig.objectsToManipulate[0]);
                break;
                
            // Add more cases for additional tasks as needed

            default:
                Debug.LogWarning("No specific pre-completion logic defined for task: " + triggerConfig.taskNameToComplete);
                break;
        }
    }
    private void CompleteTask6_PreLogic(GameObject ob)
    {
        ob.SetActive(true);

    }
    private void CompleteTask17_PreLogic(GameObject ob)
    {
       
        if (ob.GetComponent<Animator>())
            ob.GetComponent<Animator>().SetTrigger("Close");
    }
    private void CompleteTask17_PostLogic()
    {

        if (Endcanvas)
        {

            canvas.SetActive(false);
            Endcanvas.SetActive(true);

        }
    }
    private void PostCompletionLogic(TriggerConfigNitr triggerConfig)
    {
        switch (triggerConfig.taskNameToComplete)
        {
            case "Task 2.2":
                CompleteTask2_2_PostLogic(triggerConfig);
                break;

            case "Task 2":
                CompleteTask2_PostLogic(triggerConfig);
                break;

            case "Task 3":
                CompleteTask3_PostLogic();
                break;
            case "Task 7.1":
                CompleteTask7_1_PostLogic(triggerConfig.objectsToManipulate[0]);
            break;
            case "Task 8":
                CompleteTask8_PostLogic(triggerConfig);
                break;
            case "Task 8.1":
                CompleteTask8_1_PostLogic(triggerConfig);
                break;
            case "Task 6":
                CompleteTask6_PostLogic(triggerConfig);
                break;
            case "Task 17":
                CompleteTask17_PostLogic();
                break;
            // Add more cases for additional tasks as needed

            default:
                Debug.LogWarning("No specific post-completion logic defined for task: " + triggerConfig.taskNameToComplete);
                break;
        }
    }
    private void CompleteTask2_2_PostLogic(TriggerConfigNitr triggerConfig)
    {
        
        triggerConfig.objectsToManipulate[0].SetActive(true);
    }
    private void CompleteTask6_PostLogic(TriggerConfigNitr triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].SetActive(false);
        triggerConfig.objectsToManipulate[1].SetActive(true);

    }
    private void CompleteTask12_PostLogic(TriggerConfig triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].gameObject.SetActive(true);
    }
    private void CompleteTask4_PreLogic(TriggerConfig triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].gameObject.SetActive(false);
        triggerConfig.objectsToManipulate[2].gameObject.SetActive(false);
        triggerConfig.objectsToManipulate[1].gameObject.SetActive(true);
        

    }
    // Example methods for specific pre-completion logic
    private void CompleteTask8_PreLogic(TriggerConfigNitr triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].SetActive(false);

    }

    private void CompleteTask8_PostLogic(TriggerConfigNitr triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].SetActive(true);

    }
    private void CompleteTask8_1_PreLogic(TriggerConfigNitr triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].SetActive(false);
        triggerConfig.objectsToManipulate[1].SetActive(true);



    }
    private void CompleteTask8_1_PostLogic(TriggerConfigNitr triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].SetActive(false);
        triggerConfig.objectsToManipulate[1].SetActive(true);



    }
    private void CompleteTask2_PreLogic(TriggerConfigNitr OB)
    {
        // Logic specific to Task2 pre-completion
     
        OB.objectsToManipulate[0].gameObject.SetActive(true);
     
        
    }

    private void CompleteTask3_PreLogic(GameObject OB)
    {
        OB.gameObject.SetActive(true);
    }
    private void CompleteTask5_PreLogic(GameObject OB)
    {
        OB.GetComponent<Animator>().SetTrigger("Tube");
    }
    private void CompleteTask7_1_PostLogic(GameObject ob)
    {
        
        if (ob.GetComponent<Animator>())
            ob.GetComponent<Animator>().SetTrigger("Open");


    }
    // Coroutine to rotate the magnet for a given number of rotations

    // Coroutine to rotate the magnet over time
    // Coroutine to rotate the magnet once (360 degrees) over a duration

    // Example methods for post completion logic
    private void CompleteTask1_PostLogic()
    {
     
        // Logic specific to Task1 post-completion
        Debug.Log("Post-completion logic for Task1 executed.");
    }

    private void CompleteTask7_PostLogic(TriggerConfig triggerConfig){
        triggerConfig.objectsToManipulate[0].gameObject.SetActive(false);
        triggerConfig.objectsToManipulate[1].gameObject.SetActive(true);

    }
    private void CompleteTask10_PostLogic(TriggerConfig triggerConfig)
    {
        triggerConfig.objectsToManipulate[0].gameObject.SetActive(true);
       

    }
    private void CompleteTask2_PostLogic(TriggerConfigNitr triggerConfig)
    {
        canvas.SetActive(false);
        triggerConfig.objectsToManipulate[0].SetActive(false);
        triggerConfig.objectsToManipulate[1].SetActive(true);
        // Logic specific to Task2 post-completion
        Debug.Log("Post-completion logic for Task2 executed.");
    }

    private void CompleteTask3_PostLogic()
    {
        // Logic specific to Task3 post-completion
        Debug.Log("Post-completion logic for Task3 executed.");
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
