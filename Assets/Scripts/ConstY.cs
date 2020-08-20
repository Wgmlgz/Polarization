using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstY : MonoBehaviour
{
    [SerializeField] public float offset;

    private GameObject main;
    private Phantom phantom;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        phantom = GameObject.FindGameObjectWithTag("Phantom").GetComponent<Phantom>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision == main.GetComponent<CapsuleCollider2D>())
        {
            phantom.constY = true;
            phantom.constYValue = transform.position.y + offset;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            phantom.constY = false;
        }
    }
}
