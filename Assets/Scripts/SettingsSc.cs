using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsSc : MonoBehaviour
{
    [SerializeField] public Slider sliderButtonsTransparency;
    public Toggle vibrationToggle;
    public Toggle fpsToggle;


    public void ChangeToggleVibration()
    {
        PlayerPrefs.SetInt("Vibration", !vibrationToggle.isOn ? 1 : 0);
    }
    public void ChangeToggleFps()
    {
        PlayerPrefs.SetInt("Fps", fpsToggle.isOn ? 1 : 0);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Scenes/Settings");
    }
    public void ChangeButtonsTransparency(float value){
        Debug.Log(value);
        PlayerPrefs.SetFloat("buttonsTransparency", value);
    }
    
    public void SetLanguage(int lang)
    {
        PlayerPrefs.SetInt("Language", lang);
        ResetScene();
    }
    void Start()
    {
        sliderButtonsTransparency.value = PlayerPrefs.GetFloat("buttonsTransparency");
        fpsToggle.isOn = (PlayerPrefs.GetInt("Fps") != 0);
        vibrationToggle.isOn = (PlayerPrefs.GetInt("Vibration") == 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
