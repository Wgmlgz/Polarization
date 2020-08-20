using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    [SerializeField] float VALUE;
    [SerializeField] float VALUE_SPEED;
    [HideInInspector] public bool flip;

    [HideInInspector] public bool constY;
    [HideInInspector] public float constYValue;
    private GameObject main;

    public void ResetPos()
    {
        transform.position = main.transform.position;
    }
    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
        transform.position = main.transform.position;
    }

    void Update()
    {
        Vector3 targetPos = main.transform.position;
        targetPos += (Vector3)(main.GetComponent<Rigidbody2D>().velocity * VALUE_SPEED * 60f * Time.deltaTime);
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, VALUE);

        if (constY)
        {
            newPos.y = constYValue;
        }

        transform.position = newPos;
        GetComponent<SpriteRenderer>().flipX = flip;
    }
}
