using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class st_menu : MonoBehaviour
{
    [SerializeField] public string DEFAULT_LVL;

    void Start()
    {
        if (PlayerPrefs.HasKey("buttonsTransparency") == false)
        {
            PlayerPrefs.SetFloat("buttonsTransparency", 0.5f);
        }
    }

    public void ToGame()
    {
        PlayerPrefs.SetString("ContinueLvl", DEFAULT_LVL);
        //SceneManager.LoadScene("lvl1");
        if (PlayerPrefs.HasKey("ContinueLvl"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLvl"));
        }
        else
        {
            SceneManager.LoadScene(DEFAULT_LVL);
        }
    }

    public void Settings()
    {
        SceneManager.LoadScene("Scenes/Settings");
    }

    public void About()
    {
        SceneManager.LoadScene("Scenes/About");
    }

    public void ToMain()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }


    public void LoadLvl(string lvl)
    {
        PlayerPrefs.SetInt("CheckpointIndex", 0);
        SceneManager.LoadScene(lvl);
    }

    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
