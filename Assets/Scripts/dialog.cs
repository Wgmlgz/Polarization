using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class dialog : MonoBehaviour
{
    public dialogNPC npc;

    private Canvas simpleUi;
    private Canvas sequenceUi;
    public int dialogPointer;

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    void Start()
    {
        simpleUi = GetChildWithName(this.gameObject, "simple").GetComponent<Canvas>();
        sequenceUi = GetChildWithName(this.gameObject, "sequence").GetComponent<Canvas>();

        this.GetComponent<Canvas>().enabled = false;
    }
    private void DisplaySimple(string content)
    {
        simpleUi.enabled = true;
        sequenceUi.enabled = false;

        TMPro.TextMeshProUGUI displaytext = GetChildWithName(simpleUi.gameObject, "text").GetComponent<TMPro.TextMeshProUGUI>();
        displaytext.SetText(content);
        displaytext.gameObject.GetComponent<TextAnimation>().StartAnimation(content, npc.charTime, npc.charSound);
    }
    private void DisplaySequence(string content1, string content2)
    {
        simpleUi.enabled = false;
        sequenceUi.enabled = true;

        GameObject s1 = GetChildWithName(sequenceUi.gameObject, "s1");
        GameObject s2 = GetChildWithName(sequenceUi.gameObject, "s2");

        GetChildWithName(s1.gameObject, "text").GetComponent<TMPro.TextMeshProUGUI>().SetText(content1);
        GetChildWithName(s2.gameObject, "text").GetComponent<TMPro.TextMeshProUGUI>().SetText(content2);

        if(content1.Length > content2.Length)
        {
            GetChildWithName(s1.gameObject, "text").GetComponent<TextAnimation>().StartAnimation(content1, npc.charTime, npc.charSound);
        }
        else
        {
            GetChildWithName(s2.gameObject, "text").GetComponent<TextAnimation>().StartAnimation(content2, npc.charTime, npc.charSound);
        }
    }
    public void Display() {
        //Debug.Log(dialogPointer);
        string content = npc.dialogs.GetValue(dialogPointer).ToString();
        string[] data = npc.dialogs.GetValue(dialogPointer).ToString().Split(';');
        if (data[0] == "{")
        {
            System.Convert.ToInt32(data[1]);
            DisplaySequence(data[3], data[4]);
        }
        else if (data[0] == "[")
        {
            npc.events[System.Convert.ToInt32(data[1])].Invoke();
            DisplayNext(0);
        }
        else if (data[0] == "%")
        {
            dialogPointer = System.Convert.ToInt32(data[1]);
            Display();
        }
        else
        {
            DisplaySimple(content);
        }
    }
    public void DisplayNext(int mode)
    {
        string[] data = npc.dialogs.GetValue(dialogPointer).ToString().Split(';');
        if (mode == 0)
        {
            dialogPointer++;
        }
        else {
            if (mode == 1)
            {
                dialogPointer = System.Convert.ToInt32(data[1]);
            }
            else
            {
                dialogPointer = System.Convert.ToInt32(data[2]);
            }
        }

        string content = npc.dialogs.GetValue(dialogPointer).ToString();
        if (content == "END")
        {
            GetComponent<Canvas>().transform.localScale = new Vector3(0, 0, 0);
            GetComponent<Canvas>().enabled = false;

            CameraFollow cam = (GameObject.FindGameObjectWithTag("MainCamera")).GetComponent<CameraFollow>();
            cam.doForcePosDialog = false;
        }
        else
        {
            Display();
        }
    }
    public void NewDialog(dialogNPC newNPC)
    {
        npc = newNPC;
        dialogPointer = npc.startDialogPointer;
        Display();
    }
}
