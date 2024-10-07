using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicPHValue : MonoBehaviour
{

    [SerializeField] private TMP_InputField measurementsText;
    
    private float minRange = 4.00f;
    private float maxRange = 4.99f;

        
    
    private void OnEnable()
    {
        
        float randomPHValue = Random.Range(minRange, maxRange);
        
        string truncatedFloat = randomPHValue.ToString("0.00");

        measurementsText.text = "- Temperature : 25.93\u00b0\n- Potential hydrogen: " + truncatedFloat +
                                " pH\n- Conductivity : 4.71 mS/cm\n- Salinity: 1.8 ppt\n- Dissolved oxygen: 8.87 mg/l";

    }
    
    
    
    
    
    
    
    
    
    
    
    
}
