using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhisicsMat : MonoBehaviour
{
    [SerializeField] public main_script main;
    [SerializeField] public PhysicsMaterial2D newMat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            var collider = (CapsuleCollider2D)(main.player.GetComponent<CapsuleCollider2D>());
            collider.sharedMaterial = newMat;
            main.GetComponent<movement>();
        }
    }
}
