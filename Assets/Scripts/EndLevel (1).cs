using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] public string NEXT_LEVEL;

    private GameObject main;

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            PlayerPrefs.SetString("ContinueLvl", NEXT_LEVEL);
            PlayerPrefs.SetInt("CheckpointIndex", 0);
            SceneManager.LoadScene(NEXT_LEVEL);
        }
    }
}
