using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] public bool move = true;
    [SerializeField] public float movingTime;
    [SerializeField] public bool doOnce = false;
    [SerializeField] public GameObject[] targets;
    private float currentMovingTime;
    private Vector3 basePos;
    private GameObject main;

    public float vel;
    // Start is called before the first frame update
    void Start()
    {
        basePos = this.transform.position;
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            // calculate path
            float wayLen = 0;
            Vector3 lastPos = basePos;
            for (int i = 0; i < targets.Length; ++i)
            {
                Vector3 tmpPos = targets[i].transform.position;
                wayLen += (lastPos - tmpPos).magnitude;
                lastPos = tmpPos;
            }

            // moving
            Vector3 returnPos;
            currentMovingTime += Time.deltaTime;
            if (currentMovingTime >= movingTime)
            {
                if (doOnce)
                {
                    currentMovingTime = movingTime;
                    //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                    this.transform.position = targets[targets.Length - 1].transform.position;
                }
                else
                {
                    currentMovingTime = 0;
                }
            }
            float goalWayLen = currentMovingTime / movingTime * wayLen;

            vel = wayLen / movingTime;
            float tmpWayLen = 0;
            lastPos = basePos;
            for (int i = 0; i < targets.Length; ++i)
            {
                Vector3 tmpPos = targets[i].transform.position;
                tmpWayLen += (lastPos - tmpPos).magnitude;

                if (tmpWayLen > goalWayLen)
                {
                    tmpWayLen -= (lastPos - tmpPos).magnitude;
                    goalWayLen -= tmpWayLen;

                    returnPos = (tmpPos - lastPos) * (goalWayLen / (lastPos - tmpPos).magnitude) + lastPos;


                    Vector3 tmpPos1 = returnPos - this.transform.position;
                    //gameObject.GetComponent<Rigidbody2D>().velocity = tmpPos1 * 20;
                    gameObject.transform.position += tmpPos1;
                    //this.transform.Translate(tmpPos1);
                    //main.GetComponent<movement>().FixedUpdate();
                    break;
                }
                lastPos = tmpPos;
            }
        }
    }
}
