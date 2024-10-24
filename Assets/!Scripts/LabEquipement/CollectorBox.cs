using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For regular Text UI
using TMPro;
public class CollectorBox : MonoBehaviour
{
    // Start is called before the first frame update
    public TaskManager taskManager;


  

    private void OnTriggerEnter(Collider other)
    {
        GameObject enteringObject = other.gameObject;
        taskManager.CompleteTaskByName(enteringObject.name);
    }
}
