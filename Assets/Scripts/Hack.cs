using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : MonoBehaviour
{
    public GameObject controller;
    public GameObject display1;
    public GameObject display2;
    public int cursorPos = 5;

    public int size;
    public string[] cmd;
    public int[] t1;
    public int[] t2;

    public string[] Bcmd;
    public int[] Bt1;
    public int[] Bt2;

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }
    public string GenData(int start, int end)
    {
        string tmpStr = "";

        for (int i = start; i < end; ++i)
        {
            tmpStr += Reverse(System.Convert.ToString(i, 2));
            for (int j = 0; j < 8 - System.Convert.ToString(i, 2).Length; ++j) tmpStr += "0";
            tmpStr += "\t";


            tmpStr += i.ToString();
            tmpStr += "\t";

            if (i == cursorPos) tmpStr += " > ";
            else tmpStr += "    ";

            string action = cmd[i];

            if      (action == "j") tmpStr += "JMP";
            else if (action == "r") tmpStr += "RMV";
            else if (action == "l") tmpStr += "LMV";
            else if (action == "s") tmpStr += "STP";
            else if (action == "a") tmpStr += "DSH";

            tmpStr += "\t";
            tmpStr += t1[i].ToString();   
            tmpStr += "\t";
            tmpStr += t2[i].ToString();
            tmpStr += "\n";
        }


        return tmpStr;
    }
    public void GenData2()
    {
        string tmpStr = "";

        for (int i = 0; i < size; ++i)
        {
            tmpStr += t1[i];
            tmpStr += ".";
            tmpStr += t2[i];
            tmpStr += ";";
            tmpStr += cmd[i];
            if(i != size -1) tmpStr += "\n";
        }

        controller.GetComponent<AutoCharacterController>().commands = tmpStr;
        controller.GetComponent<AutoCharacterController>().GenerateCommandSecuence();
        controller.GetComponent<AutoCharacterController>().RunSequence();
    }
    //move cursor
    public void MCU()
    {
        cursorPos -= 1;
        if (cursorPos < 0) cursorPos = size - 1;
    }
    public void MCD()
    {
        cursorPos += 1;
        if (cursorPos >= size) cursorPos = 0;
    }
    public void SCP(int i)
    {
        i %= size;
        cursorPos = i;
    }

    void Start()
    {

    }

    public void Solve1()
    {
        for(int i =0; i < 16; ++i)
        {
            cmd[i] = Bcmd[i];
            t1[i] = Bt1[i];
            t2[i] = Bt2[i];
        }
    }

    public void Solve2()
    {
        for (int i = 16; i < 32; ++i)
        {
            cmd[i] = Bcmd[i];
            t1[i] = Bt1[i];
            t2[i] = Bt2[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        display1.GetComponent<TMPro.TextMeshProUGUI>().text = GenData(0, 16);
        display2.GetComponent<TMPro.TextMeshProUGUI>().text = GenData(16, 32);
    }

}
