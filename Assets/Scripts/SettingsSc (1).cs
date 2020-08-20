using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsSc : MonoBehaviour
{
    [SerializeField] public Slider sliderButtonsTransparency;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
