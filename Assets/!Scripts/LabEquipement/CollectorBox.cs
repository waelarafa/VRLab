using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For regular Text UI
using TMPro;
public class CollectorBox : MonoBehaviour
{
    // Start is called before the first frame update
    public TaskManager taskManager;
    [SerializeField] private int nbBottle;
    private int nbBottleIn=0;




    private void OnTriggerEnter(Collider other)
    {
        GameObject enteringObject = other.gameObject;
        if (enteringObject.name == "Bottle")
        {
            nbBottleIn += 1;
            if (nbBottleIn == nbBottle)
            {
                taskManager.CompleteTaskByName(enteringObject.name);
            }
        }
        else { taskManager.CompleteTaskByName(enteringObject.name); }
       
    }
}
