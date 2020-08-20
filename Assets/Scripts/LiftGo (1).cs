using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiftGo : MonoBehaviour
{
    public GameObject liftLocker;
    public GameObject phantom;
    public float moveTime = 1;
    private GameObject main;


    private bool go = true;
    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
        moveTime += gameObject.GetComponent<MovingPlatform>().movingTime;
        phantom.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void EndLift()
    {
        liftLocker.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>() && go)
        {
            gameObject.GetComponent<MovingPlatform>().move = true;
            liftLocker.SetActive(true);
            AudioManager.AudioManager.m_instance.PlaySFX("Elevator");
            go = false;
            Invoke("EndLift", moveTime);
        }
    }
}
