using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ui : MonoBehaviour
{
    [SerializeField] private Canvas startPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toGame()
    {
        Debug.Log("ttttt");
        //startPage.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
