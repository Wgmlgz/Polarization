using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpVisualizer : MonoBehaviour
{
    GameObject main;
    public GameObject cat;
    public bool doCat = false;
    float hp;
    [Header("Visual settings")]
    public bool invert = true;

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
        main = GameObject.FindGameObjectWithTag("main");
    }

    // Update is called once per frame
    void Update()
    {
        hp = main.GetComponent<main_script>().hp;
        float tmp = (1f - hp);
        if (invert == false)
        {
            tmp = hp;
        }
        GetComponent<Slider>().SetValueWithoutNotify(tmp);
    }
}
