using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3 : MonoBehaviour
{

    [SerializeField] public GameObject forceCam;
    [SerializeField] public GameObject cam;
    [SerializeField] public GameObject white;
    [SerializeField] public GameObject black;
    [SerializeField] public GameObject triangle;

    [SerializeField] float effLen;
    [SerializeField] float startWhite;
    [SerializeField] float startEff;
    [SerializeField] float moveC;
    //[SerializeField] float startShake;
    //[SerializeField] float startShake;
    //[SerializeField] float startShake;

    [SerializeField] bool activateEffect = false;
    float sourceEffectTime = 0f;
    public void ActivateDoubleJump()
    {

        activateEffect = true;
        GameObject main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
        main.GetComponent<CharacterController2D>().maxJumpCount = 2;
        PlayerPrefs.SetInt("CanDoubleJump", 1);
    }

    private void Update()
    {
        //Omg Effects
        forceCam.SetActive(activateEffect);
        GameObject.FindGameObjectWithTag("main").GetComponent<movement>().isParalyzed = activateEffect;
        if (activateEffect)
        {
            //time math
            sourceEffectTime += Time.deltaTime;
            float effectTime = sourceEffectTime - startEff;
            if(effectTime < 0)
                effectTime = 0;

            if (sourceEffectTime - startEff < effLen)
            {
                //white
                float whiteVal = ((effectTime - startWhite) > 0 ? (effectTime - startWhite) : 0) / (effLen - startWhite);
                white.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, whiteVal);

                //black
                float blackVal = effectTime / effLen;
                black.GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, blackVal);

                //shake
                float shakeVal = (effectTime < startWhite ? effectTime : startWhite) / (startWhite);
                cam.GetComponent<CameraFollow>().shake = true;
                cam.GetComponent<CameraFollow>().shakeRange = shakeVal * 1f;

                //triangle
                float moveVal = effectTime / effLen;
                triangle.GetComponent<BackMove>().posOffset.y = moveVal * moveC;
            }
            else
            {
                activateEffect = false;
                sourceEffectTime = 0;

                white.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 0f);
                black.GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, 0f);
                cam.GetComponent<CameraFollow>().shake = false;
                cam.GetComponent<CameraFollow>().shakeRange = 0f;
                cam.GetComponent<CameraFollow>().ResetPos(true);
                triangle.GetComponent<BackMove>().posOffset.y = 0f;
            }
        }
        else
        {
            activateEffect = false;
            sourceEffectTime = 0;
        }
    }
}
