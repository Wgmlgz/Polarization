using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] float speed = 1;
    private Transform target;

    private GameObject main;
    private GameObject pod;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        AudioManager.AudioManager.m_instance.PlaySFX("Key");
    }
    public void GoSleep()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        pod = GameObject.FindGameObjectWithTag("Pod");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == main.GetComponent<CapsuleCollider2D>())
        {
            pod.GetComponent<FollowRobot>().Addkey(gameObject);
        }
    }

    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }
    }
}
