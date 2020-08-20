using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveerMover : MonoBehaviour
{
    private GameObject main;
    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
