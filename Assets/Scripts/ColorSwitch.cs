using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    [SerializeField] public GameObject[] objs;
    [SerializeField] public Color color;

    private Color oldColor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (color != oldColor)
        {

            Debug.Log("Changing color");
            foreach (var i in objs)
            {
                i.GetComponent<SpriteRenderer>().color = color;
            }
        }
        oldColor = color;
    }
}
