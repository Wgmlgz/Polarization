using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class dialog : MonoBehaviour
{
    public dialogNPC npc;

    private Canvas simpleUi;
    private Canvas sequenceUi;
    private int dialogPointer;

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
        displaytext.gameObject.GetComponent<TextAnimation>().StartAnimation(content, npc.charSound);
    }
    private void DisplaySequence(string content1, string content2)
    {
        simpleUi.enabled = false;
        sequenceUi.enabled = true;

        GameObject s1 = GetChildWithName(sequenceUi.gameObject, "s1");
        GameObject s2 = GetChildWithName(sequenceUi.gameObject, "s2");

        GetChildWithName(s1.gameObject, "text").GetComponent<TMPro.TextMeshProUGUI>().SetText(content1);
        GetChildWithName(s2.gameObject, "text").GetComponent<TMPro.TextMeshProUGUI>().SetText(content2);

        GetChildWithName(s1.gameObject, "text").GetComponent<TextAnimation>().StartAnimation(content1);
        GetChildWithName(s2.gameObject, "text").GetComponent<TextAnimation>().StartAnimation(content2);
    }
    public void Display() {
        string content = npc.dialogs.GetValue(dialogPointer).ToString();
        string[] data = npc.dialogs.GetValue(dialogPointer).ToString().Split(';');
        if (data[0] == "{")
        {
            System.Convert.ToInt32(data[1]);
            DisplaySequence(data[3], data[4]);
        }else if (data[0] == "[")
        {
            npc.events[System.Convert.ToInt32(data[1])].Invoke();
            DisplayNext(0);
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

            CameraFollow cam = ((GameObject)GameObject.FindGameObjectsWithTag("MainCamera").GetValue(0)).GetComponent<CameraFollow>();
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

    public void test()
    {
        bool t = this.GetComponent<Canvas>().enabled;

        t = !t;

        this.GetComponent<Canvas>().enabled = t;
    }
}
