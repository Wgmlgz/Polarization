using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zhabka : MonoBehaviour
{
    private GameObject main;

    public float size = .25f;
    public int sizeInt = 0;
    public float addSize = .05f;

    public GameObject indicator;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");

        if (PlayerPrefs.HasKey("zhabka_size"))
            SetSize(PlayerPrefs.GetFloat("zhabka_size"));
        else
            SetSize(size);

        if (PlayerPrefs.HasKey("zhabka_size_int"))
            sizeInt = PlayerPrefs.GetInt("zhabka_size_int");
        else
            PlayerPrefs.SetInt("zhabka_size_int", sizeInt);
        
    }

    void SetSize(float newSize)
    {
        size = newSize;
        PlayerPrefs.SetFloat("zhabka_size", newSize);
        transform.localScale = new Vector3(newSize, newSize, newSize);

        Vector3 tmp = transform.position;
        tmp.y = (newSize * 2f) + 16.5f;
        transform.position = tmp;

        float t = 1f / transform.localScale.x;
        indicator.transform.localScale = new Vector3(t, t, t);
    }

    public void AddSize()
    {
        if (main.GetComponent<main_script>().coins > 0)
        {
            main.GetComponent<main_script>().RemoveCoin();
            SetSize(size + addSize);
            sizeInt += 1;
            PlayerPrefs.SetInt("zhabka_size_int", sizeInt);
        }
    }
    
    void Update()
    {
        
    }
}
