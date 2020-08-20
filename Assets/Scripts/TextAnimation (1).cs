using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using AudioManager;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] public float charTime = 0.05f;
    [SerializeField] public string charSound = "DefaultCharSound";
    [SerializeField] public int charsToSound = 15;
    private string text;

    private float animTime = 0f;
    private bool anim = false;

    private int lastSound = 0;

    public void StartAnimation(string newText = null, string sound = "DefaultCharSound")
    {
        charSound = sound;
        animTime = 0f;
        lastSound = 0;
        if (newText == null)
        {
            text = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
        }
        else
        {
            text = newText;
        }
        anim = true;
        AudioManager.AudioManager.m_instance.PlaySFX(charSound);
    }

    void SetText(string text)
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }
    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            animTime += Time.deltaTime;

            int charsToShow = Mathf.RoundToInt(animTime / charTime);
            if (charsToShow > text.Length)
            {
                anim = false;
                SetText(text);
            }
            else
            {
                string showText = text.Substring(0, charsToShow);
                if (charsToShow - lastSound >= charsToSound)
                {
                    Debug.Log(charsToShow - lastSound);
                    Debug.Log(charsToSound);
                    AudioManager.AudioManager.m_instance.PlaySFX(charSound);
                    lastSound = charsToShow;
                }
                SetText(showText);
            }
        }
    }
}
