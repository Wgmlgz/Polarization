using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove : MonoBehaviour
{
    [SerializeField] float moveScale;
    [SerializeField] Vector3 offset;
    [SerializeField] public Vector3 posOffset;
    Vector3 basePos;
    private GameObject main;
    private Vector3 oldOffset;

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("MainCamera").GetValue(0);
        basePos = this.transform.position;
        //YScale = moveScale;
    }

    void Update()
    {
        oldOffset += offset * Time.deltaTime;
        Vector3 tmpPos = main.transform.position - basePos + oldOffset;
        tmpPos.z = 0;

        tmpPos.x *= moveScale;
        tmpPos.y *= moveScale;
        tmpPos += basePos;

        tmpPos += posOffset;
        this.transform.position = tmpPos;
    }
}
