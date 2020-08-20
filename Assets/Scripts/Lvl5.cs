using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5 : MonoBehaviour
{
    [SerializeField] GameObject angleObj1;
    [SerializeField] GameObject angleObj2;
    [SerializeField] GameObject sw1;
    [SerializeField] GameObject sw2;
    [SerializeField] GameObject sw3;
    [SerializeField] GameObject sw4;
    [SerializeField] GameObject sw5;
    [SerializeField] GameObject lazer1;
    [SerializeField] GameObject lazer2;
    [SerializeField] GameObject lock1;
    [SerializeField] GameObject lock2;
    [SerializeField] GameObject key;
    [SerializeField] LiftGo lift;

    public bool active4;
    public bool active5;
    public void SetAngle1(float angle)
    {
        Quaternion t = Quaternion.Euler(0f, 0f, angle);
        angleObj1.transform.rotation = t;
    }
    public void Change4(bool state)
    {
        active4 = state;
    }
    public void Change5(bool state)
    {
        active5 = state;
    }
    public void DeathRefresh()
    {
        sw2.GetComponent<OneZero>().value = 0;
    }
    public void GiveKey()
    {
        key.SetActive(true);
    }
    private void Update()
    {
        if (active4 == false)
        {
            sw4.GetComponent<OneZero>().value = 0;
            sw4.GetComponent<OneZero>().ChangeCablesColor();
        }
        if (active5 == false)
        {
            sw5.GetComponent<OneZero>().value = 0;
            sw5.GetComponent<OneZero>().ChangeCablesColor();
        }

        lazer1.SetActive(sw2.GetComponent<OneZero>().value == 1);
        lazer2.SetActive(sw2.GetComponent<OneZero>().value != 1);

        lock1.SetActive(sw4.GetComponent<OneZero>().value != 1);
        lock2.SetActive(sw5.GetComponent<OneZero>().value != 1);

        lift.enabled = sw1.GetComponent<OneZero>().value == 0;
    }

}
