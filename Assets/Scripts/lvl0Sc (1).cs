using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvl0Sc : MonoBehaviour
{
    public GameObject cat;
    public GameObject portal;

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

    public void MoveCat()
    {
        GetChildWithName(cat, "Dialog0").SetActive(false);
        Invoke("MoveCat1", .5f);
        Invoke("MoveCat2", 1);
        Invoke("MoveCat3", 11.5f);
        Invoke("MoveCat4", 12);
    }

    private void MoveCat1()
    {
        cat.GetComponent<SpriteRenderer>().flipX = true;
    }
    private void MoveCat2()
    {
        cat.GetComponent<MovingPlatform>().move = true;
    }
    private void MoveCat3()
    {
        cat.GetComponent<SpriteRenderer>().flipX = false;
    }
    private void MoveCat4()
    {
        portal.SetActive(true);
    }


    public void Tutorial1()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
