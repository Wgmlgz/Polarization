using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS : MonoBehaviour
{
    public GameObject H;
    public bool[] b = {false, false, false, false, false, false, false, false};
    
    public void SetB(int n, bool val)
    {
        b[n] = val;
    }
    public void Set0(bool val)
    {
        SetB(0, val);
    }
    public void Set1(bool val)
    {
        SetB(1, val);
    }
    public void Set2(bool val)
    {
        SetB(2, val);
    }
    public void Set3(bool val)
    {
        SetB(3, val);
    }
    public void Set4(bool val)
    {
        SetB(4, val);
    }
    public void Set5(bool val)
    {
        SetB(5, val);
    }
    public void Set6(bool val)
    {
        SetB(6, val);
    }
    public void Set7(bool val)
    {
        SetB(7, val);
    }

    public void SetC()
    {
        H.GetComponent<Hack>().SCP(GetInt());
    }
    public void SetA()
    {
        H.GetComponent<Hack>().cmd[H.GetComponent<Hack>().cursorPos] = "l";
        if      (GetInt() == 0) H.GetComponent<Hack>().cmd[H.GetComponent<Hack>().cursorPos] = "s";
        else if (GetInt() == 1) H.GetComponent<Hack>().cmd[H.GetComponent<Hack>().cursorPos] = "j";
        else if (GetInt() == 2) H.GetComponent<Hack>().cmd[H.GetComponent<Hack>().cursorPos] = "r";
        else if (GetInt() == 3) H.GetComponent<Hack>().cmd[H.GetComponent<Hack>().cursorPos] = "l";
        else if (GetInt() == 4) H.GetComponent<Hack>().cmd[H.GetComponent<Hack>().cursorPos] = "a";
    }
    public void SetT()
    {
        H.GetComponent<Hack>().t1[H.GetComponent<Hack>().cursorPos] = GetInt();
    }
    public void SetT2()
    {
        H.GetComponent<Hack>().t2[H.GetComponent<Hack>().cursorPos] = GetInt();
    }
    public int GetInt()
    {
        int t = 0;
        int g = 1;

        for(int i = 0; i < 8; ++i)
        {
            t += b[i] ? g : 0;
            g *= 2;
        }

        return t;
    }
}
