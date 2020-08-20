using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [Range(0f, 24f)] public float time;
    [Range(0f, 24f)] public float timeSpeed;
    [SerializeField] Color[] colors;
    [SerializeField] float[] times;
    private GameObject camera;
    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + timeSpeed * Time.deltaTime) % 24f;
        float minV = 99999999;
        int tmpCol = 0;

        int col1;
        int col2;
        for(int i = 0; i < colors.Length; ++i)
        {
            if(Mathf.Abs(times[i] - time) < minV && times[i] <= time)
            {
                tmpCol = i;
                minV = Mathf.Abs(times[i] - time);
            }
        }
        col1 = tmpCol;
        minV = 99999999;
        for (int i = 0; i < colors.Length; ++i)
        {
            if (Mathf.Abs(times[i] - time) < minV && times[i] > time)
            {
                tmpCol = i;
                minV = Mathf.Abs(times[i] - time);
            }
        }
        col2 = tmpCol;
        if(col1 > col2)
        {
            tmpCol = col1;
            col1 = col2;
            col2 = tmpCol;
        }
        Debug.Log(col1);
        Debug.Log(col2);
        Debug.Log((time - times[col1]) / (times[col2] - times[col1]));
        Color newBackground = Color.Lerp(colors[col1], colors[col2], (time - times[col1]) / (times[col2] - times[col1]));
        camera.GetComponent<Camera>().backgroundColor = newBackground;
    }
}
