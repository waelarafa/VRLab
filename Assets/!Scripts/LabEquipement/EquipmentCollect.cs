using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BNG;
using Unity.VisualScripting;
[System.Serializable]
public class GameObjectTextPair
{
    public GameObject gameObject;
    public TextMeshProUGUI textMeshProUGUI;
}
public class EquipmentCollect : MonoBehaviour
{
    public Canvas TasksCanvas;

    public List<GameObjectTextPair> equipmentListWithUI = new List<GameObjectTextPair>();

    private Dictionary<GameObject, TextMeshProUGUI> textByGameObject = new Dictionary<GameObject, TextMeshProUGUI>();

  


    void Start()
    {
        
            foreach (GameObjectTextPair pair in equipmentListWithUI)
        {
            if (pair.gameObject != null && pair.textMeshProUGUI != null)
            {
                textByGameObject[pair.gameObject] = pair.textMeshProUGUI;
            }
            else
            {
                Debug.LogWarning("One of the GameObjectTextPairs has a null value.");
            }
        }

    }



    public void Update()
    {
        if (InputBridge.Instance.XButtonDown)
        {
            TasksCanvas.enabled = true;
        }
        if (InputBridge.Instance.XButtonUp)
        {
            TasksCanvas.enabled = false;
        }
    }

    public void TaskValidate(GameObject Equipement)
    {
        textByGameObject[Equipement].text = Equipement.name + " : 1/1";

    }

}
