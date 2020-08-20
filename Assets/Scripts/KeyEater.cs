using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyEater : MonoBehaviour
{
    [SerializeField] public UnityEvent OnKeyUse;

    private bool canUse = true;
    private GameObject main;
    private GameObject pod;

    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        pod = GameObject.FindGameObjectWithTag("Pod");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>() && canUse)
        {
            if (pod.GetComponent<FollowRobot>().HasKey()) {
                pod.GetComponent<FollowRobot>().RemoveKey();
                canUse = false;
                OnKeyUse.Invoke();
                AudioManager.AudioManager.m_instance.PlaySFX("KeyOff");
            }
        }
    }
}
