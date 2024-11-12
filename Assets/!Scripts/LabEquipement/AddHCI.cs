using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class AddHCI : MonoBehaviour
{
    private bool isIn = false;
    [SerializeField] private GameObject volume;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropParent; // Parent to organize instantiated drops in the hierarchy
    private int dropNb = 0;
    [SerializeField] private  int maxDrops = 10;
    [SerializeField] private GameObject objectToEnable; // Object to enable after reaching max drops
   

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MixerButton" && !isIn && dropNb < maxDrops)
        {
            isIn = true;
           
            InstantiateDrop();
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MixerButton")
        {
            isIn = false;
        }
    }

    private void InstantiateDrop()
    {
        Instantiate(dropPrefab, dropParent.position, dropParent.rotation, dropParent); // Instantiate drop at specified position
        dropNb++;
        volume.GetComponent<TMP_InputField>().text = dropNb.ToString(); // Update text with current drop count
                                                                        // Check if maximum drops are reached
        if (dropNb == maxDrops)
        {
            objectToEnable.SetActive(true);  // Enable the specified object
          // Disable the specified object
        }
    }

    // Start is called before the first frame update

}
