using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class st_menu : MonoBehaviour
{
    [SerializeField] public string DEFAULT_LVL;
    [SerializeField] TMPro.TextMeshProUGUI statsText;
    [SerializeField] TMPro.TextMeshProUGUI statsTextNorm;
    [SerializeField] string[] codes;
    [SerializeField] int[] codesCoins;
    [SerializeField] TMPro.TextMeshProUGUI ttt;
    public UnityEngine.UI.Slider sl;

    public float time;
    public bool ignoreMusic = false;

    #region vibration
    public void SetSlTime()
    {
        time = sl.value;
    }
    public void VibrateTime()
    {
        Vibration.Vibrate(Mathf.RoundToInt(time * 1000));
    }
    public void TapPopVibrate()
    {
        Vibration.VibratePop();
    }

    public void TapPeekVibrate()
    {
        Vibration.VibratePeek();
    }

    public void TapNopeVibrate()
    {
        Vibration.VibrateNope();
    }
    #endregion vibration
    #region main
    public void CopyToClipboard()
    {
        string s = statsText.text;
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
    }
    void Start()
    {

        
        if (PlayerPrefs.HasKey("buttonsTransparency") == false)
        {
            PlayerPrefs.SetFloat("buttonsTransparency", 0.5f);
        }
        if (statsText != null)
        {
            string t = "";

            t += "game," + PlayerPrefs.GetInt("_game_deaths").ToString() + "," + PlayerPrefs.GetFloat("_game_time").ToString();
            t += "\n";
            t += "\n";

            for (int i = 0; i < 21; ++i)
            {
                float g = PlayerPrefs.GetFloat("_lvl_time_Lvl" + i.ToString());
                t += "lvl_" + i.ToString() + "," + PlayerPrefs.GetInt("_lvl_deaths_Lvl" + i.ToString()) + ",";
                t += System.Math.Round(g);
                t += "\n";
            }

            statsText.text = t;
        }
        if (statsTextNorm != null)
        {
            string t = "";

            int lang = PlayerPrefs.GetInt("Language");
            //     (lang == 1 ? "" : "")
            t += (lang == 1 ? "Статистика по игре:\n\n" : "Game statistics:\n\n");

            t += (lang == 1 ? "Время в игре: " : "Game time: ") + System.Math.Round(PlayerPrefs.GetFloat("_game_time")).ToString()
                + (lang == 1 ? " c." : " s.") + "\n";
            t += (lang == 1 ? "Прыжков: " : "Jumps: ") + PlayerPrefs.GetInt("_game_jumps").ToString() + "\n";
            t += (lang == 1 ? "Смертей: " : "Deaths: ") + PlayerPrefs.GetInt("_game_deaths").ToString() + "\n";
            t += (lang == 1 ? "Монет собранно: " : "Coins collected: ") + PlayerPrefs.GetInt("_game_coins").ToString() + "\n";
            t += (lang == 1 ? "Жабка скушала монет: " : "Toad ate coins: ") + PlayerPrefs.GetInt("zhabka_size_int").ToString() + "\n";

            t += "\n";
            t += (lang == 1 ? "Собрано монет на уровнях: " : "Collected coins in levels: ") + "\n";
            t += "\n";

            for (int jj = 0, j = 10; jj < 11 && j < 21; ++jj, ++j)
            {
                //float g = PlayerPrefs.GetFloat("_lvl_time_Lvl" + i.ToString());
                //t += "lvl_" + i.ToString() + "," + PlayerPrefs.GetInt("_lvl_deaths_Lvl" + i.ToString()) + ",";
                //t += System.Math.Round(g);

                int i = jj;
                t += i.ToString() + ": \t";
                t += PlayerPrefs.GetInt("_lvl_coins_Lvl" + i.ToString()) + " / ";
                t += (PlayerPrefs.HasKey("_lvl_maxcoins_Lvl" + i.ToString()) ? PlayerPrefs.GetInt("_lvl_maxcoins_Lvl" + i.ToString()).ToString() : "???");
                t += "\t";

                i = j;
                t += i.ToString() + ": \t";
                t += PlayerPrefs.GetInt("_lvl_coins_Lvl" + i.ToString()) + " / ";
                t += (PlayerPrefs.HasKey("_lvl_maxcoins_Lvl" + i.ToString()) ? PlayerPrefs.GetInt("_lvl_maxcoins_Lvl" + i.ToString()).ToString() : "???");
                t += "\n";
            }

            statsTextNorm.text = t;
        }
        if (!ignoreMusic)
        {
            PlayerPrefs.SetFloat("last_ad_time", 0f);
            AudioManager.AudioManager.m_instance.PlayMusic("Menu");
        }
    }
    public void ToGame()
    {
        if (PlayerPrefs.HasKey("ContinueLvl"))
        {
            Debug.Log(PlayerPrefs.GetString("ContinueLvl"));
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
    public void ResetScene()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
    public void SetLanguage(int lang)
    {
        PlayerPrefs.SetInt("Language", lang);
        ResetScene();
    }
    public void openURL(string url)
    {
        Application.OpenURL(url);
    }
    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void EnterCode(string s)
    {
        for(int i = 0; i< codes.Length; ++i)
        {
            if(s == codes[i] && PlayerPrefs.GetInt("code_" + codes[i]) == 0)
            {
                PlayerPrefs.SetInt("code_" + codes[i], 1);
                PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins") + codesCoins[i]));
                ttt.text = "You wsdad" + codesCoins[i].ToString();
                ttt.SetText(
                PlayerPrefs.GetInt("Language") == 1 ?
                    "Лови " + codesCoins[i].ToString() + " монет!" :
                    "Catch " + codesCoins[i].ToString() + " coins!"
                );
                return;
            }
        }
    }
    #endregion main
}
