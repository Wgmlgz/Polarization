using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSc : MonoBehaviour
{
    public bool DoLandOnce = false;
    private bool isLand = false;
    public UnityEvent OnLandEvent;

    public bool DoLeaveOnce = false;
    private bool isLeave = false;
    public UnityEvent OnLeaveEvent;

    public Collider2D customCollider = null;
    private GameObject main;

    /*private void Awake()
    {
        //m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnLeaveEventt == null)
            OnLeaveEvent = new UnityEvent();
    }*/

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLand == false)
        {
            if ((collision == main.GetComponent<CapsuleCollider2D>() && customCollider == null) ||
                (collision == customCollider && customCollider != null)
                )
            {
                OnLandEvent.Invoke();
            }
        }

        if (DoLandOnce) isLand = true;
    }

    /*
    private void OnTriggerExit(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            public UnityEvent OnLandEvent;
}
    }*/
}
