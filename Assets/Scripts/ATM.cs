using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;

public class ATM : MonoBehaviour
{
    GameObject main;
    dialog dia;
    public int value = 10;
    public UnityEvent doOnBuy;
    public bool buyOnce;
    public int flagBuyOnce = 10;
    public int flagSuccess = 10;
    public int flagNotEnoughMoney = 10;
    public int GOVNo = 10;
    public TMPro.TextMeshProUGUI text;
    [Header("forSkins")]
        public bool useForSkins = false;
        public int skinID = 0;
        public GameObject mainATM;
    [Header("forIAP")]
    public bool doIAP = false;
    public string nameIAP;

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    public void Buy()
    {
        int coins = main.GetComponent<main_script>().coins;
        if (coins >= value)
        {
            main.GetComponent<main_script>().coins -= value;
            if (doOnBuy != null)
            {
                if (useForSkins)
                {
                    GetComponent<ActionByVar>().SetName("unlock_skin" + skinID.ToString());
                    GetComponent<ActionByVar>().SetBool(1);
                }
                doOnBuy.Invoke();
            }
            if (buyOnce)
            {
                GetComponent<dialogNPC>().startDialogPointer = flagBuyOnce;
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_atm_lock_" + transform.position.x + "_" + transform.position.x, 1);
            }
            MovePointer(flagSuccess);
        }
        else
        {
            MovePointer(flagNotEnoughMoney);
        }
    }
    public void BuyByAd()
    {
        main.GetComponent<LvlScripts>().ActiveZhabka = this.gameObject;

        value = 1999999999;
        Buy();

        AdManager.AdManager.m_instance.ShowRewardedAd();
    }
    public void FinishBuyByAd(bool adWatch)
    {
        Debug.Log("FinishBuyByAd");
        if (adWatch)
        {
            value = 0;
        }
        else
        {
            value = 99999;
        }
        Buy();
    }
    void MovePointer(int flag)
    {
        flag -= 1;
        dia.dialogPointer = flag;
        dia.Display();
    }
    public void SetSkin()
    {
        if (PlayerPrefs.GetInt("unlock_skin" + skinID.ToString()) != 0)
        {
            main.GetComponent<main_script>().ChangeSkin(skinID);
        }
    }
    public string GetPrice(string productID)
    {
        string s = "";
        //s = 
        return s;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_atm_lock_" + transform.position.x + "_" + transform.position.x) == 1)
        {
            GetComponent<dialogNPC>().startDialogPointer = flagBuyOnce;
        }
        main = GameObject.FindGameObjectWithTag("main");
        dia = GameObject.FindGameObjectWithTag("dialogUi").GetComponent<dialog>();

        if (doIAP)
        {
        }
        else text.SetText(value.ToString());

        if (useForSkins)
        {
            GetChildWithName(GetChildWithName(mainATM, "Canvas1"), "SkinName").GetComponent<TextLocalization>().RU = main.GetComponent<main_script>().skinNamesRU[skinID];
            GetChildWithName(GetChildWithName(mainATM, "Canvas1"), "SkinName").GetComponent<TextLocalization>().EN = main.GetComponent<main_script>().skinNamesEN[skinID];
            GetChildWithName(GetChildWithName(mainATM, "Canvas1"), "SkinName").GetComponent<TextLocalization>().Refresh();
            GetChildWithName(GetChildWithName(mainATM, "Canvas"), "Text").GetComponent<ActionByVar>().boolName = "unlock_skin" + skinID.ToString();
            GetComponent<ActionByVar>().boolName = "unlock_skin" + skinID.ToString();
            GetChildWithName(mainATM, "Sprite").GetComponent<SpriteRenderer>().sprite = main.GetComponent<main_script>().skins[skinID];
            GetChildWithName(mainATM, "SpriteB").GetComponent<SpriteRenderer>().sprite = main.GetComponent<main_script>().skins[skinID];
            GetChildWithName(mainATM, "Sprite").GetComponent<ActionByVar>().boolName = "unlock_skin" + skinID.ToString();
        }
    }
    void Update()
    {
    }
}
