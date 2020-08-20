using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_script : MonoBehaviour
{
    [SerializeField] public bool debug = true;
    [SerializeField] public Transform player;
    [SerializeField] public GameObject resp;

    [SerializeField] public GameObject[] checkpoints;
    [SerializeField] public int coins;

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
        this.transform.position = respawnPoint.transform.position;
        if (respawnPoint.GetComponent<checkpoint_script>().doCam)
        {
            ((GameObject)GameObject.FindGameObjectsWithTag("MainCamera").GetValue(0)).GetComponent<CameraFollow>().ResetPos(respawnPoint.GetComponent<checkpoint_script>().lookDirection);
        }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("Coins") == false)
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
        else
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        if (debug == false)
        {
            respawn(checkpoints[PlayerPrefs.GetInt("CheckpointIndex")]);
        }
    }
}
