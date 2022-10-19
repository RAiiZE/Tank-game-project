using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowLightButton : MonoBehaviour
{
    public bool rainbowOn;
    public Text toggle;

    public void RainbowToggle()
    {
        if (rainbowOn)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<rainbowSpotty>().enabled = true;
            toggle.text = "Rainbow Light: On";
        }

        
    }
}
