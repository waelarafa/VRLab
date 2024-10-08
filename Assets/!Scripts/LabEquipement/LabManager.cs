using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BNG;

public class TaskManager : MonoBehaviour
{
    public GameObject taskPrefab; // Assign the Task prefab
    public Transform taskPanel; // Assign the Panel where tasks will be instantiated
    public TaskList taskList; // Reference to the TaskList ScriptableObject
    public bool checkFirstItem;
    public Color taskTextColor; // Exposed color for task text
    private int tasksLength;
    private float progressRate;
    private float TotalProgress;
    public TextMeshProUGUI progressUI;
    public Canvas TaskCanva;
    public Canvas endCanva;
    private void Start()
    {
        tasksLength = taskList.tasks.Length;
        progressRate = 100 / tasksLength;
        for (int i = 0; i < tasksLength; i++)
        {
            // Set the toggle state based on the task index
            bool isChecked = (i == 0 && checkFirstItem); // Only check the first task
            CreateTask(taskList.tasks[i].taskName, taskList.tasks[i].taskDescription, isChecked);
        }
    }

    // Create a task with task name, description, and a checked state
    void CreateTask(string taskName, string taskDescription, bool isChecked)
    {
        GameObject taskObj = Instantiate(taskPrefab, taskPanel);

        // Set the task GameObject's name to the taskName from the ScriptableObject
        taskObj.name = taskName;

        TaskItem taskItem = taskObj.GetComponent<TaskItem>();
        taskItem.SetTaskDescription(taskDescription); // Set task description

        // Apply the custom text color to the task text
        taskItem.SetTaskTextColor(taskTextColor);

        // Set the toggle state
        taskItem.toggle.isOn = isChecked; // Check the toggle for the first task
        taskItem.toggle.interactable = false; // Disable user interaction

        // Add listener to handle task completion
        taskItem.toggle.onValueChanged.AddListener(delegate { OnTaskToggle(taskItem.toggle); });
    }

    // Handle task toggle change
    void OnTaskToggle(Toggle toggle)
    {
        // Logic when a task toggle is changed
    }

    // Call this function to complete a task programmatically by its GameObject name (which is the taskName)
    public void CompleteTaskByName(string taskName)
    {
        foreach (Transform child in taskPanel)
        {
            // Check if the GameObject's name matches the task name
            if (child.gameObject.name == taskName)
            {
                TaskItem taskItem = child.GetComponent<TaskItem>();
                if (taskItem != null)
                {
                    taskItem.CompleteTask(); // Mark task as completed
                    Debug.Log(taskName + " is completed.");
                }
                return;
            }
        }

        Debug.Log("Task not found: " + taskName);
    }
    public void upgradeProgress()
    {
        progressUI.text = "progress " + TotalProgress + "%";
        TotalProgress += progressRate;
        if (TotalProgress == 100)
        {
            endCanva.enabled = true;
        }
    }
    public void Update()
    {
        if (InputBridge.Instance.XButtonDown)
        {
            TaskCanva.enabled = true;
        }
        if (InputBridge.Instance.XButtonUp)
        {
            TaskCanva.enabled = false;
        }
    }
}
