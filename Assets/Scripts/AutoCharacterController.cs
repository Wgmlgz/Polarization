using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCharacterController : MonoBehaviour
{
    private GameObject main;

    public bool generateByText;
    [TextArea] public string commands;
    public string[] commandSequence;

    public bool interpritete = false;
    private bool interpriteteFlag = false;
    public float interpritationTimer = 0f;
    private int tmpCommand = 0;
    public string[] tmpActions;
    public float[] tmpDelays;
    public GameObject restart;

    [Header("Debug")]
    public bool doJump;
    public bool doAct;
    public bool doLeft;
    public bool doRight;
    public bool doStop;

    public void GenerateCommandSecuence()
    {
        string[] tmpText = commands.Split('\n');
        tmpText.CopyTo(commandSequence, 0);
    }

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }

    public void RunSequence()
    {
        tmpCommand = 0;
        interpritete = true;
        interpritationTimer = 0;

        float tmpTime = 0;
        for (int i = 0; i < commandSequence.Length; ++i)
        {

            string[] tmpData = commandSequence[i].Split(';');

            float time = (float)System.Convert.ToDouble(tmpData[0]);
            tmpTime += time;
            tmpDelays.SetValue(tmpTime, i);
            Debug.Log("--" + tmpData[1] + "--");
            tmpActions.SetValue(tmpData[1], i);
        }
    }
    void RunAction(string action)
    {
        if(action == "j")
        {
            Jump();
        }
        else if(action == "r")
        {
            Right();
        }
        else if (action == "l")
        {
            Left();
        }
        else if (action == "s")
        {
            Stop();
        }
        else if (action == "a")
        {
            Debug.Log("act");
            Act();
        }
    }
    void Jump()
    {
        main.GetComponent<movement>().JumpInput();
    }
    void Act()
    {
        main.GetComponent<movement>().ActInput();
    }
    void Left()
    {
        main.GetComponent<movement>().SetHorizontalInput(-1f);
    }
    void Right()
    {
        main.GetComponent<movement>().SetHorizontalInput(1f);
    }
    void Stop()
    {
        main.GetComponent<movement>().SetHorizontalInput(0f);
    }
    void Update()
    {
        if (generateByText) GenerateCommandSecuence();
        //debug
        if (doJump)
        {
            Jump();
        }
        if (doAct)
        {
            Act();
        }
        if (doLeft)
        {
            Left();
        }
        if (doRight)
        {
            Right();
        }
        if (doStop)
        {
            Stop();
        }

        //interpritator
        if (interpritete)
        {
            if(interpritete != interpriteteFlag)
            {
                RunSequence();
            }
            interpritationTimer += Time.deltaTime;
            for (int i = tmpCommand; i < commandSequence.Length; ++i)
            {
                if(tmpDelays[i] < interpritationTimer)
                {
                    tmpCommand = i + 1;
                    RunAction(tmpActions[i]);
                }
            }
        }
        interpriteteFlag = interpritete;

        restart.gameObject.SetActive(interpritete);
    }
}
