using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHub : MonoBehaviour
{
    GameObject main;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        if (PlayerPrefs.GetInt("CanDoubleJump") == 1)
        {
            main.GetComponent<CharacterController2D>().maxJumpCount = 2;
        }
        if (PlayerPrefs.GetInt("Lvl19unlock") == 1)
        {
            main.GetComponent<CharacterController2D>().maxJumpCount = 9999999;
        }

        main.GetComponent<CharacterController2D>().maxDashes = PlayerPrefs.GetInt("max_dashes");
    }

    void GiveSkin(int n)
    {
        PlayerPrefs.SetInt("unlock_skin" + n.ToString(), 1);
        main.GetComponent<main_script>().ChangeSkin(n);
    }
    
    public void SetHardcoreMode(bool b)
    {
        PlayerPrefs.SetInt("hardcore", b?1:0);
        main.GetComponent<main_script>().RefreshTr(b);
    }
    void Update()
    {
        
    }
}
