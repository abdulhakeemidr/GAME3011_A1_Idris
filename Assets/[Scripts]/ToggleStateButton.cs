using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleStateButton : MonoBehaviour
{
    Text toggleText;
    Toggle toggleButton;
    void Start()
    {
        toggleText = GetComponentInChildren<Text>();
        toggleButton = GetComponent<Toggle>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ScanMode)
        {
            toggleText.text = "Scan Mode";
        }
        else if(GridOrganizer.instance.slotState == ResourceSlotState.ExtractMode)
        {
            toggleText.text = "Extract Mode";
        }
    }


}
