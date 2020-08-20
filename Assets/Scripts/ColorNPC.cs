using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorNPC : MonoBehaviour
{
    [Header("Base")]
    public GameObject colorIndicator;
    public Color[] colors;
    public int activeColor = 1;


    [Header("Dialog")]
    private int language = 0;
    public string[] namesEN;
    public string[] namesRU;

    public string startStrEN = "Set color to ";
    public string startStrRU = "Поменять цвет на ";

    public string endStrEN = " ";
    public string endStrRU = " ";

    public string nextStrEN = "Set another color";
    public string nextStrRU = "Выбрать другой цвет";

    public string cancelStrEN = "Cancel";
    public string cancelStrRU = "Выход";

    public string[] generatedDialog;

    string[] DialogGenerator(string[] names, string startStr, string endStr, string nextStr, string cancelStr)
    {
        //string[] generatedDialog = null;
        //generatedDialog.Initialize();

        for (int i = 0; i < names.Length; ++i)
        {
            generatedDialog.SetValue(
                "{;" + (i + names.Length + 1).ToString() + ";" + (i + 1).ToString() + ";" +
                startStr + names[i] + endStr + ";"
                + "" + ((i == names.Length -1)? cancelStr : nextStr)
                , i);
        }
        generatedDialog[names.Length] = "END";
        for (int i = names.Length + 1; i < names.Length * 2 + 1; ++i)
        {
            generatedDialog[i] = "[;" + (i - names.Length - 1);
        }
        Debug.Log(generatedDialog);
        return generatedDialog;
    }

    void Start()
    {
        SimpleChangeColor(activeColor);
        language = PlayerPrefs.GetInt("Language");

        dialogNPC dia = GetComponent<dialogNPC>();

        if (language == 1)
        {
            DialogGenerator(namesRU, startStrRU, endStrRU, nextStrRU, cancelStrRU).CopyTo(dia.RU, 0);
        }
        else
        {
            DialogGenerator(namesEN, startStrEN, endStrEN, nextStrEN, cancelStrEN).CopyTo(dia.EN, 0);
        }
    }

    private void SimpleChangeColor(int newColor)
    {
        activeColor = newColor;
        colorIndicator.GetComponent<SpriteRenderer>().color = colors[newColor];
    }
    public void ChangeColor(int newColor)
    {
        SimpleChangeColor(newColor);

        int flag = namesEN.Length;
        dialog dia = GameObject.FindGameObjectWithTag("dialogUi").GetComponent<dialog>();
        flag -= 1;
        dia.dialogPointer = flag;
        dia.Display();
    }

    void Update()
    {
    }
}
