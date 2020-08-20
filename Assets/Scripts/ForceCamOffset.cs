using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCamOffset : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] public bool left;
    [SerializeField] public bool center;
    [SerializeField] public bool right;

    [SerializeField] public bool forceY;
    [SerializeField] public bool constPos;
    [SerializeField] public Vector3 pos;
    [SerializeField] public bool doNewSize;
    [SerializeField] public float newCamSize;

    private GameObject main;
    private CameraFollow cam;

    private float oldCamSize;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        if (target)
        {
            pos = target.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            oldCamSize = cam.gameObject.GetComponent<Camera>().orthographicSize;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {

            if (doNewSize)
            {
                cam.gameObject.GetComponent<Camera>().orthographicSize = newCamSize;
            }
            //pos
            if (constPos)
            {
                pos.z = cam.forcePos.z;
                cam.forcePos = pos;
                cam.doForcePos = true;
            }
            else if(left || center || right)
            {
                Vector3 newOffset = cam.lookOffset;
                if (left)
                {
                    newOffset.x *= -1;
                }
                else if (center)
                {
                    newOffset.x = 0;
                }
                cam.forceOffset = newOffset;
                cam.doForceOffset = true;
            }
            if (forceY)
            {
                cam.forcePos = pos;
                cam.forceY = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {

            cam.gameObject.GetComponent<Camera>().orthographicSize = oldCamSize;

            if (constPos)
            {
                cam.doForcePos = false;
            }
            else
            {
                cam.doForceOffset = false;
            }
            if (forceY)
                cam.forceY = false;
        }
    }
}
