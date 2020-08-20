using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class st_ui : MonoBehaviour
{
    [SerializeField] private string level = "Launch";
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
        SceneManager.LoadScene(level);
        //startPage.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
