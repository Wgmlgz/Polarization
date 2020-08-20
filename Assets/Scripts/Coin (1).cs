using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] Vector2 collectForce = new Vector2(0, 1000f);
    private GameObject main;
    private bool collected = false;
    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);

    }

    void Collect()
    {
        AudioManager.AudioManager.m_instance.PlaySFX(1);

        this.gameObject.AddComponent(typeof(Rigidbody2D));
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 8f;
        this.GetComponent<Rigidbody2D>().AddRelativeForce(collectForce);
    }
    void VihodVOkno()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == (Collider2D)main.GetComponent<CapsuleCollider2D>() && collected == false)
        {
            collected = true;
            main.GetComponent<main_script>().AddCoin();
            Invoke("Collect", 0);
            Invoke("VihodVOkno", 5);
        }
    }
}
