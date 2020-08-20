using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class main_script : MonoBehaviour
{
    [SerializeField] public bool debug = true;
    [SerializeField] public bool BlockInput = false;

    [Header("skins")]
    [SerializeField] public Sprite[] skins;
    [SerializeField] public int skinID;
    [SerializeField] public string[] skinNamesEN;
    [SerializeField] public string[] skinNamesRU;


    [Header("Other")]
    [SerializeField] public Transform player;
    [HideInInspector] public GameObject resp;
    [SerializeField] public GameObject[] checkpoints;
    [SerializeField] public int coins;
    [SerializeField] UnityEvent deathEvent;
    public GameObject tr;
    public int stBan = 1;

    [Header("Rain")]
        public float hp = 1f;
        public bool doRainDamage = false;
        public float rainDamage = 0.1f;
        public bool healByTime = true;
        public float healValue = 0.1f;

    private GameObject restartScreen;

    //shit code
    public GameOverlay GO;
    public bool waitTp = false;
    public bool adComplite = false;

    void RewriteCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }
    public void AddCoin()
    {
        coins += 1;
        RewriteCoins();
    }
    public void RemoveCoin()
    {
        coins -= 1;
        RewriteCoins();
    }
    public void respawn(GameObject respawnPoint)
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.parent = null;
        this.transform.position = respawnPoint.transform.position;
        GetComponent<CharacterController2D>().jumpsLeft = GetComponent<CharacterController2D>().maxJumpCount;
        GetComponent<CharacterController2D>().dashesLeft = GetComponent<CharacterController2D>().maxDashes;
        GameObject.FindGameObjectWithTag("Phantom").GetComponent<Phantom>().ResetPos();

        if (deathEvent != null)
            deathEvent.Invoke();
        if (respawnPoint.GetComponent<checkpoint_script>().doCam)
        {
            (GameObject.FindGameObjectWithTag("MainCamera")).GetComponent<CameraFollow>().ResetPos(respawnPoint.GetComponent<checkpoint_script>().lookDirection);
        }
        /*
        Debug.Log("st_ad");
        InterstitialAd ad = new InterstitialAd(AdRevard);
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("AE367AD48BAEFF52").Build();
        ad.LoadAd(request);
        if (ad.IsLoaded())
        {
            ad.Show();
            Debug.Log("da");
        }*/
    }
    public void ChangeSkin(int id)
    {
        Debug.Log(id);
        skinID = id;
        PlayerPrefs.SetInt("active_skin", id);
        GetComponent<SpriteRenderer>().sprite = skins[skinID];
        GameObject.FindGameObjectWithTag("Phantom").GetComponent<SpriteRenderer>().sprite = skins[skinID];
        Debug.Log(id);
    }
    void Start()
    {
        if(PlayerPrefs.HasKey("max_dashes") == false)
        {
            PlayerPrefs.SetInt("max_dashes", 0);
        }
        if(GetComponent<CharacterController2D>().maxDashes > PlayerPrefs.GetInt("max_dashes"))
        {
            PlayerPrefs.SetInt("max_dashes", GetComponent<CharacterController2D>().maxDashes);
        }

        PlayerPrefs.SetString("ContinueLvl", SceneManager.GetActiveScene().name);
        restartScreen = (GameObject)GameObject.FindGameObjectsWithTag("Restart").GetValue(0);
        ChangeSkin(PlayerPrefs.GetInt("active_skin"));

        if (PlayerPrefs.HasKey("Coins") == false) PlayerPrefs.SetInt("Coins", 0);
        else coins = PlayerPrefs.GetInt("Coins");

        if (PlayerPrefs.GetInt("hardcore") == 1 && SceneManager.GetActiveScene().name != "MainHub")
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "CheckpointIndex", 0);
            for (int i = stBan; i < checkpoints.Length; ++i)
            {
                checkpoints[i].SetActive(false);
                //checkpoints[i] = checkpoints[0];
            }
        }

        RefreshTr(PlayerPrefs.GetInt("hardcore") == 1);

        if (debug == false)
        {
            respawn(checkpoints[PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "CheckpointIndex")]);
        }
    }
    public void RefreshTr(bool b)
    {
        tr.SetActive(b);
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("last_ad_time", PlayerPrefs.GetFloat("last_ad_time") + Time.deltaTime);

        if (waitTp && adComplite)
        {
            adComplite = false;
            waitTp = false;
            GO.NextCheckpoint();
            Debug.Log("next");
        }
        if (hp < 0f)
        {
            hp = 0f;
            Death();
        }
        if (healByTime)
        {
            hp += healValue * Time.deltaTime;
            if (hp > 1f)
            {
                hp = 1f;
            }
        }

        if(transform.position.y < -9999)
        {
            Death();
        }

        PlayerPrefs.SetFloat("_lvl_time_" + SceneManager.GetActiveScene().name, PlayerPrefs.GetFloat("_lvl_time_" + SceneManager.GetActiveScene().name) + Time.deltaTime);
        PlayerPrefs.SetFloat("_game_time", PlayerPrefs.GetFloat("_game_time") + Time.deltaTime);
        RewriteCoins();
    }
    public void AdFin()
    {
        adComplite = true;
    }
    void OnParticleCollision(GameObject other)
    {
        if (doRainDamage)
        {
            hp -= rainDamage;
            if(hp < 0f)
            {
                hp = 0f;
                Death();
            }
        }
    }
    public void Death()
    {
        restartScreen.GetComponent<Canvas>().enabled = true;
        AudioManager.AudioManager.m_instance.PlaySFX(2);
        Time.timeScale = 0;
    }
}
