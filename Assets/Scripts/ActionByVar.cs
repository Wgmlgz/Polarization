using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionByVar : MonoBehaviour
{
    [Header("When Work?")]
        [SerializeField] bool doStart;
        [SerializeField] bool doUpdate;
    [Header("Bool Action")]
        [SerializeField] bool doByBool;
        [SerializeField] public string boolName;
        [SerializeField] UnityEvent onTrue;
        [SerializeField] UnityEvent onFalse;

    string tmpName;
    [SerializeField] UnityEvent evt;
    public void SetName(string name)
    {
        tmpName = name;
    }
    public void SetBool(int value)
    {
        PlayerPrefs.SetInt(tmpName, value);
    }
    public void DoIfBool(string name)
    {
        if (PlayerPrefs.GetInt(name)!=0)
        {
            evt.Invoke();
        }
    }
    void Refresh()
    {
        if (doByBool)
        {
            bool t = PlayerPrefs.GetInt(boolName) != 0;
            if (t)
            {
                onTrue.Invoke();
            }
            else
            {
                onFalse.Invoke();
            }
        }
    }
    void Start()
    {
        if (doStart)
        {
            Refresh();
        }
    }

    void Update()
    {
        if (doUpdate)
        {
            Refresh();
        }
    }
}
