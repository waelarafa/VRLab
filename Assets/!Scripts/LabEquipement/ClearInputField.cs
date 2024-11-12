using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClearInputField : MonoBehaviour
{
    public InputField LabelBottleInputField;
    // Start is called before the first frame update
    public void clearText()
    {
        LabelBottleInputField.text = "";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
