using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSc : MonoBehaviour
{
    [SerializeField] public main_script main;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.player.transform.position = main.resp.transform.position;
        }
    }
}
