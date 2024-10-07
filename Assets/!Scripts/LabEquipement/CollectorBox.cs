using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For regular Text UI
using TMPro;
public class CollectorBox : MonoBehaviour
{
    // Start is called before the first frame update
    public TaskManager taskManager;


    private void OnCollisionEnter(Collision collision)
    {
        // "collision" contains details about the object that collided with the box
        GameObject enteringObject = collision.gameObject;
        taskManager.CompleteTaskByName(enteringObject.name);
        taskManager.upgradeProgress();
    }


}
