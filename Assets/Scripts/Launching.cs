using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launching : MonoBehaviour
{

    public GameObject white;
    public Text text_object;
    private string text_output = "[PROTOTYPE 079]                     \nHardware Safe Mode Active          \nDeactivating Hybernation Mode...        \nRebooting Operating System...                       \nAI Operational...\nComplete. All Systems Are Ready.\nEngaging Testing Mode Sequence...\nGenerating Personal Robot Identification Code...            \nCode: XqAKr5ypA9jEJSvDK3wAbWqZ3DdKA\nStarting Teleportation Sequence...                                             \nCharging...                        \n3...                                                        \n2...                                                        \n1...                                                        \nREADY...                                                   ";
    private int i = 0;
    void Start()
    {
        white.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0F);
        text_object.text = "";
    }
    void Update()
    {
        ++i;
        int tmp = i / 3;
        string tmpstr = "";
        for(int j = 0; j < tmp ;++j)
        {
            tmpstr += text_output[j];
        }
        text_object.text = tmpstr;
        float oof = (float)(tmp - 625) / 50;
        if (tmp == 675)SceneManager.LoadScene("Scenes/Lvlselect");
        if(tmp > 625) white.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, oof);
    }
}
