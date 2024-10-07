using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillText : MonoBehaviour
{
    public ListViewItem item;
    public TextMeshProUGUI[] text;

        // Start is called before the first frame update
        void Start()
    {
        item = this.GetComponent<ListViewItem>();
        text = GetComponentsInChildren<TextMeshProUGUI>();
        text[0].text = item.row0Ref.rowText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
