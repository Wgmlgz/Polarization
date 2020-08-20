using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] Vector2 collectForce = new Vector2(0, 1000f);
    private GameObject main;
    private bool collected = false;
    void Start()
    {
        if (!PlayerPrefs.HasKey("_lvl_maxcoins_" + SceneManager.GetActiveScene().name))
        {
            Coin[] coins = Object.FindObjectsOfType<Coin>();
            PlayerPrefs.SetInt("_lvl_maxcoins_" + SceneManager.GetActiveScene().name, coins.Length);
        }


        main = GameObject.FindGameObjectWithTag("main");
        if(PlayerPrefs.GetInt("coin_" +
            SceneManager.GetActiveScene().name +
            "_" + transform.position.x.ToString() +
            "_" + transform.position.y.ToString()
        ) == 1)
        {
            Destroy(gameObject);
        }
    }

    public UnityEvent onCollect;
    void Collect()
    {
        Vibration.VibratePop();
        PlayerPrefs.SetInt("coin_" +
            SceneManager.GetActiveScene().name +
            "_" + transform.position.x.ToString() +
            "_" + transform.position.y.ToString(),
            1);
        AudioManager.AudioManager.m_instance.PlaySFX(1);

        this.gameObject.AddComponent(typeof(Rigidbody2D));
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 8f;
        this.GetComponent<Rigidbody2D>().AddRelativeForce(collectForce);
        onCollect.Invoke();
    }
    void VihodVOkno()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == (Collider2D)main.GetComponent<CapsuleCollider2D>() && collected == false)
        {
            PlayerPrefs.SetInt("_game_coins", PlayerPrefs.GetInt("_game_coins") + 1);
            PlayerPrefs.SetInt("_lvl_coins_" + SceneManager.GetActiveScene().name, PlayerPrefs.GetInt("_lvl_coins_" + SceneManager.GetActiveScene().name) + 1);

            collected = true;
            if (PlayerPrefs.GetInt("hardcore") == 1)
            {
                main.GetComponent<main_script>().AddCoin();
                main.GetComponent<main_script>().AddCoin();
                main.GetComponent<main_script>().AddCoin();
            }
            else
            {
                main.GetComponent<main_script>().AddCoin();
            }
            Invoke("Collect", 0);
            Invoke("VihodVOkno", 5);
        }
    }
}
