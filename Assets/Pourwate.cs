using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pourwate : MonoBehaviour
{ 
    private bool waterDroped=false;
    public GameObject water;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BottleRinzing collider trigred :" + other.gameObject.name);
        if (other.CompareTag("Drop") && !waterDroped)
        {
            Debug.Log("Multiparameter collider trigred :");
            waterDroped = true;

            //other.transform.GetChild(0).gameObject.SetActive(true);
            water.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
