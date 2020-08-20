using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Kostil : MonoBehaviour
{
    public UnityEvent Action;

    public AdManager.AdManager adm;


    private void Start()
    {
        
    }
    void Update()
    {
        if(adm == null) adm = AdManager.AdManager.m_instance;
        if (adm.b)
        {
            Debug.Log("bBBVBBBBBBBBBB~~~!!!!");
            adm.b = false;
            Action.Invoke();
        }
    }
}
