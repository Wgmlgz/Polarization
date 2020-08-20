using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInput : MonoBehaviour
{
    private GameObject main;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        main.GetComponent<main_script>().BlockInput = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        main.GetComponent<main_script>().BlockInput = false;
    }
}
