using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] public string NEXT_LEVEL;
    [SerializeField] public int unlock;
    [SerializeField] public bool noAd = false;
    private GameObject main;

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            Vibration.VibratePeek();
            if (SceneManager.GetActiveScene().name != "MainHub") PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "CheckpointIndex", 0);
            //AudioManager.AudioManager.m_instance.PlaySFX("Portal");
            PlayerPrefs.SetInt("Lvl" + unlock.ToString() + "unlock", 1);
            SceneManager.LoadScene(NEXT_LEVEL);
            if (PlayerPrefs.GetInt("DisableAds") != 1 && !noAd)
            {
                AdManager.AdManager.m_instance.ShowRewardedAd();
            }
        }
    }
}
