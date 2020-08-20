using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkpoint_script : MonoBehaviour
{
    //[SerializeField] private Transform player;
    private main_script main;
    [SerializeField] public bool dontOpen = false;
    [SerializeField] public bool doCam = true;
    [SerializeField] public bool lookDirection = true;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite newSprite;
    [SerializeField] public PhysicsMaterial2D defaultMat;
    [SerializeField] public GameObject part;
    [SerializeField] public bool ignorePart = false;
   private bool open = false;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main").GetComponent<main_script>();
        this.gameObject.AddComponent(typeof(ChangePhisicsMat));
        ((ChangePhisicsMat)(GetComponent<ChangePhisicsMat>())).newMat = defaultMat;
        ((ChangePhisicsMat)(GetComponent<ChangePhisicsMat>())).main = main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == GameObject.FindGameObjectWithTag("main").GetComponent<CapsuleCollider2D>());
        {
            if (!open)
            {
                if (ignorePart == false) Vibration.VibratePeek();
                main.resp = this.gameObject;
                if (dontOpen == false)
                {
                    open = true;
                    sprite.sprite = newSprite;
                    if (Time.timeSinceLevelLoad > 2f) AudioManager.AudioManager.m_instance.PlaySFX("Checkpoint");
                }
                
                if(ignorePart == false) part.GetComponent<ParticleSystem>().Play();
                for (int j = 0; j < main.checkpoints.Length; ++j)
                {
                    var i = main.GetComponent<main_script>().checkpoints[j];
                    if (i == this.gameObject)
                    {
                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "CheckpointIndex", j);
                    }
                }
            }
        }
    }
}
