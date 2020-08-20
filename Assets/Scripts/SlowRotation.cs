using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    [Range(-500f, 500f)] public float rotationSpeed = 90;
    [SerializeField] bool useRandom = false;
    [SerializeField] float minValue = 0f;
    [SerializeField] float maxValue = 0f;
    [SerializeField] bool genNewValues = false;
    private bool direction;
    private float curSpeed;
    void updateValues()
    {
        direction = (Random.value > .5f);
        curSpeed = Random.value * (maxValue - minValue) + minValue;
    }
    private void Start()
    {
        updateValues();
    }
    void Update()
    {
        if (genNewValues) updateValues();

        Quaternion t = transform.rotation;
        if (useRandom == false)
        {
            t = Quaternion.Euler(0, 0, t.eulerAngles.z + rotationSpeed * Time.deltaTime);
        }
        else
        {
            if(direction) t = Quaternion.Euler(0, 0, t.eulerAngles.z + curSpeed * Time.deltaTime);
            else t = Quaternion.Euler(0, 0, t.eulerAngles.z - curSpeed * Time.deltaTime);
        }
        transform.rotation = t;
    }
}
