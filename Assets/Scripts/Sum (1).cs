using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sum : MonoBehaviour
{
    
    [SerializeField] GameObject[] cables; 
    [SerializeField] GameObject[] switches;
    [SerializeField] GameObject resultDisplay;
    [SerializeField] GameObject onOffObj;
    [SerializeField] int goal = 228;
    private int value;
    void Update()
    {
        value = 0;
        for(int i = 0; i < switches.Length; ++i)
        {
            value += Mathf.RoundToInt(Mathf.Pow(2, i)) * switches[i].GetComponent<OneZero>().value;
        }


        bool equal = value == goal;
        resultDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString() + (equal ? " == ": " != ") + goal.ToString();

        GetComponent<ColorSwitch>().color = 
            (equal?
            new Vector4(0.1682093f, 0.3732349f, 0.5660378f, 1):
            new Vector4(0.5372549f, 0.5372549f, 0.5372549f, 1)
            );
        onOffObj.SetActive(equal);
    }
}
