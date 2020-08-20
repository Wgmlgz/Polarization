using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlScripts : MonoBehaviour
{
    private GameObject main;

    [Header("Zhabki")]
    public bool zhabki = false;
    public GameObject key;
    public GameObject Zhabka1;
    public GameObject Zhabka2;
    public GameObject Zhabka3;
    public GameObject Zhabka4;
    public GameObject Zhabka5;
    public GameObject ActiveZhabka;
    [Range(0f,2f)] public float dropDelay = 1f;
    private bool zhabaYES = false;
    private float dropTimer = 0f;

    public GameObject[] cubes;
    public float newCubeTime = 12f;
    public GameObject smolCube;
    public Vector3 smolCubePos;

    public bool doLsdCat = false;
    public GameObject lsdCat;

    public Vector3 tpPos;

    [Header("Salt")]
    public bool doSalt = false;
    public GameObject[] saltOff;
    public GameObject[] saltOn;
    public GameObject cam;
    public bool salt = false;
    public bool doGI = false;
    public float saltSpeed = 100f;


    public bool stupidCode = false;
    public bool stupidCode1 = false;


    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }
    void DropZhaba(GameObject zhabka)
    {
        zhabka.GetComponent<Rigidbody2D>().simulated = true;
        zhabka.GetComponent<Rigidbody2D>().gravityScale = 2f;
        //zhabka.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, 100f));
        //szhabka.GetComponent<Rigidbody2D>().add  AddRelativeForce(new Vector2(0f, 100f));
    }
    void DestroyZhabki()
    {
        Destroy(Zhabka1.gameObject);
        Destroy(Zhabka2.gameObject);
        Destroy(Zhabka3.gameObject);
        Destroy(Zhabka4.gameObject);
        Destroy(Zhabka5.gameObject);
    }
    public void ZhabkaAd(bool ass)
    {
        ActiveZhabka.GetComponent<ATM>().FinishBuyByAd(true);
    }
    public void ResetLevel(int checkpoint)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResetCubes()
    {
        foreach(var i in cubes)
        {
            i.GetComponent<MovingPlatform>().currentMovingTime = newCubeTime;
        }
    }
    public void ResetSmolCube()
    {
        smolCube.transform.position = smolCubePos;
    }
    public void AddCoins(int n)
    {
        for(int i = 0; i < n; ++i)
        {
            main.GetComponent<main_script>().AddCoin();
        }
    }
    public void TpMain()
    {
        main.transform.position = tpPos;
    }
    public void SetLsd(bool b)
    {
        doLsdCat = b;
    }
    public void SetSalt(bool b)
    {
        salt = b;
    }
    void Update()
    {
        if (zhabki)
        {
            if (
                Zhabka1.GetComponent<ColorNPC>().activeColor == 1 &&
                Zhabka2.GetComponent<ColorNPC>().activeColor == 2 &&
                Zhabka3.GetComponent<ColorNPC>().activeColor == 3 &&
                Zhabka4.GetComponent<ColorNPC>().activeColor == 0 &&
                Zhabka5.GetComponent<ColorNPC>().activeColor == 4
                )
            {
                zhabaYES = true;
                key.SetActive(true);
            }

            if (zhabaYES)
            {
                dropTimer += Time.deltaTime;
                if (dropTimer > dropDelay * 0) DropZhaba(Zhabka1);
                if (dropTimer > dropDelay * 1) DropZhaba(Zhabka2);
                if (dropTimer > dropDelay * 2) DropZhaba(Zhabka3);
                if (dropTimer > dropDelay * 3) DropZhaba(Zhabka4);
                if (dropTimer > dropDelay * 4) DropZhaba(Zhabka5);
                if (dropTimer > dropDelay * 10) DestroyZhabki();
            }
        }
        if (doLsdCat)
        {
            lsdCat.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        if (doSalt)
        {
            foreach(var i in saltOff)
            {
                i.SetActive(salt == true);
            }
            foreach (var i in saltOff)
            {
                i.SetActive(salt == false);
            }
            if (salt)
            {
                if (cam.GetComponent<Animation>().isPlaying == false)
                {
                    cam.GetComponent<Animation>().Play();
                }
                main.GetComponent<movement>().runSpeed = saltSpeed;

                main.GetComponent<CharacterController2D>().ignoreJump = true;
                main.GetComponent<CharacterController2D>().doInvertGravity = true;
            }
            else
            {
                if (cam.GetComponent<Animation>().isPlaying)
                {
                    cam.GetComponent<Animation>().Stop();
                }
                cam.GetComponent<Camera>().orthographicSize = 7f;
                main.GetComponent<movement>().runSpeed = 60f;
                if (main.GetComponent<Rigidbody2D>().gravityScale < 0)
                {
                    main.GetComponent<Rigidbody2D>().gravityScale *= -1;
                }
                main.GetComponent<CharacterController2D>().ignoreJump = false;
                main.GetComponent<CharacterController2D>().doInvertGravity = false;
            }
        }
    }
}
