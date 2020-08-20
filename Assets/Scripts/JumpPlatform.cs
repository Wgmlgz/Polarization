using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [Range(1000f, 8000f)] public float force = 2500f;
    [Range(1000f, 100000f)] public float forceForOthers = 2500f;
    private GameObject main;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.GetComponent<CharacterController2D>().Jump(force);
            main.GetComponent<CharacterController2D>().jumpsLeft = main.GetComponent<CharacterController2D>().maxJumpCount;
        }
        else
        {
            Rigidbody2D m_Rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 curVelocity = m_Rigidbody2D.velocity;
            curVelocity.y = 0;
            m_Rigidbody2D.velocity = curVelocity;
            m_Rigidbody2D.AddRelativeForce(new Vector2(0f, forceForOthers));
            //Debug.Log(collision.gameObject);
        }
        AudioManager.AudioManager.m_instance.PlaySFX("JumpPlatform");
    }

}

