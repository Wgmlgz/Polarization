using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dialogNPC : MonoBehaviour
{
    [HideInInspector]public string[] dialogs;
    [SerializeField] public float charTime = 0.05f;
    [SerializeField] public string charSound = "DefaultCharSound";
    [SerializeField] public string[] EN;
    [SerializeField] public string[] RU;
    private int language = 0;

    [SerializeField] public UnityEvent[] events;

    public int startDialogPointer = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
            language = PlayerPrefs.GetInt("Language");
        else
            PlayerPrefs.SetInt("Language", 0);

        if (language == 1)
        {
            dialogs = RU;
        }
        else
        {
            dialogs = EN;
        }
    }
    public void tryDialog()
    {
        Debug.Log("OMG, main object r going to talk with " + this.name);
        GameObject dialogUi = GameObject.FindGameObjectWithTag("dialogUi");
        dialogUi.GetComponent<Canvas>().enabled = true;
        Debug.Log(dialogUi);
        dialogUi.GetComponent<dialog>().NewDialog(this);
    }
}
 