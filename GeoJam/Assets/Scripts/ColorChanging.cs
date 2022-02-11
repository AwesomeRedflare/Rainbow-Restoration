using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanging : MonoBehaviour
{
    public Text t;

    void Start()
    {
        // Initialize color, set material color using HSVToRGB.
        t.color = Color.HSVToRGB(1f, 1f, 1f);
    }
    void Update()
    {
        // Assign HSV values to float h, s & v. (Since material.color is stored in RGB)
        float h, s, v;
        Color.RGBToHSV(t.color, out h, out s, out v);

        // Use HSV values to increase H in HSVToRGB. It looks like putting a value greater than 1 will round % 1 it
        t.color = Color.HSVToRGB(h + Time.deltaTime * .25f, s, v);
    }
}
