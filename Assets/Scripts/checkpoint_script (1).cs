using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint_script : MonoBehaviour
{
    //[SerializeField] private Transform player;
    private main_script main;
    [SerializeField] public bool doCam = true;
    [SerializeField] public bool lookDirection = true;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite newSprite;
    [SerializeField] public PhysicsMaterial2D defaultMat;

    private bool open = false;

    void Start()
    {
        main = ((GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0)).GetComponent<main_script>();
        this.gameObject.AddComponent(typeof(ChangePhisicsMat));
        ((ChangePhisicsMat)(GetComponent<ChangePhisicsMat>())).newMat = defaultMat;
        ((ChangePhisicsMat)(GetComponent<ChangePhisicsMat>())).main = main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == (Collider2D)((GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0)).GetComponent<CapsuleCollider2D>());
        {
            if (!open)
            {

                sprite.sprite = newSprite;
                main.resp = this.gameObject;
                open = true;
                AudioManager.AudioManager.m_instance.PlaySFX("Checkpoint");

                for (int j = 0; j < main.checkpoints.Length; ++j)
                {
                    var i = main.GetComponent<main_script>().checkpoints[j];
                    if (i == this.gameObject)
                    {
                        PlayerPrefs.SetInt("CheckpointIndex", j);
                    }
                }
            }
        }
    }
}
