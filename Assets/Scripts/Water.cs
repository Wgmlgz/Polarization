using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Range(0f, 40f)] public float viscosity = 20f;
    private GameObject main;

    private float timeIn = 0f;
    private float timeOut = 0f;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().Stop();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().Stop();

        //audio
        if (timeIn >= 1f && timeOut >= 1f)
        {
            AudioManager.AudioManager.m_instance.PlaySFX("WaterOut");
            timeIn = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            if (timeIn >= 1f && timeOut >= 1f)
            {
                AudioManager.AudioManager.m_instance.PlaySFX("WaterIn");
                timeOut = 0f;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.GetComponent<Rigidbody2D>().velocity -= main.GetComponent<Rigidbody2D>().velocity * viscosity * Time.deltaTime;
            main.GetComponent<CharacterController2D>().jumpsLeft = main.GetComponent<CharacterController2D>().maxJumpCount;
            if(GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().isStopped)
            {
                GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().Play();
            }
            //main.GetComponent<CharacterController2D>().dashesLeft = main.GetComponent<CharacterController2D>().maxDashes;
        }
    }
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

    private void Update()
    {
        timeIn += Time.deltaTime;
        timeOut += Time.deltaTime;
    }
}
