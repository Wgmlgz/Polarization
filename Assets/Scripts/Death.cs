using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] bool hide = false;
    private GameObject main;
    private GameObject restartScreen;

    private Color oldColor;

    public float dmg = 0.1f;

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
        
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        restartScreen = GameObject.FindGameObjectWithTag("Restart");
        if (hide)
        {
            oldColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            Vibration.VibratePeek();
            if (hide)
            {
                gameObject.GetComponent<SpriteRenderer>().color = oldColor;
            }
            PlayerPrefs.SetInt("_game_deaths", PlayerPrefs.GetInt("_game_deaths") + 1);
            PlayerPrefs.SetInt("_lvl_deaths_" + SceneManager.GetActiveScene().name, PlayerPrefs.GetInt("_lvl_deaths_" + SceneManager.GetActiveScene().name) + 1);
            restartScreen.GetComponent<Canvas>().enabled = true;
            AudioManager.AudioManager.m_instance.PlaySFX("Death");
            Time.timeScale = 0;
        }
    }
}
