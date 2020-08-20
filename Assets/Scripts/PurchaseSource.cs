using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PurchaseSource : MonoBehaviour
{
    public GameObject main;
    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "disableads") PlayerPrefs.SetInt("DisableAds", 1);
        else if (product.definition.id == "coins_50") main.GetComponent<main_script>().coins += 50;
        else if (product.definition.id == "coins_100") main.GetComponent<main_script>().coins += 100;
        else if (product.definition.id == "coins_500") main.GetComponent<main_script>().coins += 500;
        else if (product.definition.id == "coins_1000") main.GetComponent<main_script>().coins += 1000;
        else if (product.definition.id == "coins_999999999") main.GetComponent<main_script>().coins += 999999999;
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product " + product.definition.id + " failed because " + reason);
    }
}

