using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl1 : MonoBehaviour
{
    [SerializeField] public bool isLiftActivated = false;
    [SerializeField] GameObject[] cables;
    [SerializeField] GameObject lift;
    [SerializeField] GameObject liftLocker;

    [SerializeField] GameObject secondLift;
    [SerializeField] GameObject secondLiftLocker;
    [SerializeField] GameObject cat;
    [SerializeField] Sprite angryCat;
    [SerializeField] Sprite normalCat;
    [SerializeField] Sprite happyCat;

    [SerializeField] GameObject tpObj = null;
    [SerializeField] float tpTime = 3;
    [SerializeField] GameObject end;
    private float tpTransparet;
    private bool tp = false;
    public void ActivateMainLift()
    {
        isLiftActivated = true;
        foreach (var i in cables){
            i.GetComponent<SpriteRenderer>().color = new Vector4(0.1682093f, 0.3732349f, 0.5660378f, 1);
        }
    }
    public void GoLift(bool playSound)
    {
        if (isLiftActivated)
        {
            liftLocker.SetActive(true);
            lift.GetComponent<MovingPlatform>().move = true;
            if (playSound)
            {
                AudioManager.AudioManager.m_instance.PlaySFX("Elevator");
            }
        }
    }
    public void GoSecondLift()
    {
        Debug.Log("secLift");
        secondLiftLocker.SetActive(true);
        secondLift.GetComponent<MovingPlatform>().move = true;
        AudioManager.AudioManager.m_instance.PlaySFX("Elevator");
        Invoke("EndSecondLift", 18);
    }
    public void EndSecondLift()
    {
        secondLiftLocker.SetActive(false);
    }
    public void DropCat()
    {
        cat.GetComponent<Rigidbody2D>().simulated = true;
    }
    public void AngryCat()
    {
        cat.GetComponent<SpriteRenderer>().sprite = angryCat;
    }
    public void NormalCat()
    {
        cat.GetComponent<SpriteRenderer>().sprite = normalCat;
    }
    public void HappyCat()
    {
        cat.GetComponent<SpriteRenderer>().sprite = happyCat;
    }
    public void Teleport()
    {
        tp = true;
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (tp)
        {
            tpTransparet += Time.deltaTime;
            tpObj.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, tpTransparet / tpTime);
            if(tpTransparet >= tpTime)
            {
                end.active = true;
            }
        }
    }
}
