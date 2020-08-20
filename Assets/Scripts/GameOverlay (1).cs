using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameOverlay : MonoBehaviour
{
    private Canvas pauseOverlay;
    private Canvas notificationOverlay;

    private GameObject main;
    private bool isPaused = false;

    private int okAction;

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

    void Start()
    {
        pauseOverlay = GetChildWithName(this.gameObject, "pauseOverlay").GetComponent<Canvas>();
        notificationOverlay = GetChildWithName(this.gameObject, "NotificationOverlay").GetComponent<Canvas>();
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
        //GetChildWithName(this.gameObject, "RestartOverlay").SetActive(false);

        Vector3 col = new Vector3(0, 0, 0);
        float buttonsTransparency = PlayerPrefs.GetFloat("buttonsTransparency");

        GetChildWithName(this.gameObject, "Left").GetComponent<Image>().color = new Vector4(col.x, col.y, col.z, buttonsTransparency);
        GetChildWithName(this.gameObject, "Right").GetComponent<Image>().color = new Vector4(col.x, col.y, col.z, buttonsTransparency);
        GetChildWithName(this.gameObject, "Jump").GetComponent<Image>().color = new Vector4(col.x, col.y, col.z, buttonsTransparency);
        GetChildWithName(this.gameObject, "Act").GetComponent<Image>().color = new Vector4(col.x, col.y, col.z, buttonsTransparency);
        GetChildWithName(this.gameObject, "PauseButton").GetComponent<Image>().color = new Vector4(col.x, col.y, col.z, buttonsTransparency);


        GetChildWithName(GetChildWithName(this.gameObject, "Left"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().alpha = buttonsTransparency;
        GetChildWithName(GetChildWithName(this.gameObject, "Right"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().alpha = buttonsTransparency;
        GetChildWithName(GetChildWithName(this.gameObject, "Jump"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().alpha = buttonsTransparency;
        GetChildWithName(GetChildWithName(this.gameObject, "Act"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().alpha = buttonsTransparency;
        GetChildWithName(GetChildWithName(this.gameObject, "PauseButton"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().alpha = buttonsTransparency;
        Resume();
    }
    void Update()
    {
        int coins = main.GetComponent<main_script>().coins;
        TMPro.TextMeshProUGUI coinCounter =
            GetChildWithName(GetChildWithName(this.gameObject, "ActOverlay"), "CoinCounter").GetComponent< TMPro.TextMeshProUGUI>();
        coinCounter.SetText(coins.ToString());
}
    public void PauseButton()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Pause()
    {
        isPaused = true;
        pauseOverlay.enabled = true;
        GetChildWithName(GetChildWithName(this.gameObject, "PauseButton"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().SetText("<");

        Time.timeScale = 0;
    }
    public void Resume()
    {
        isPaused = false;
        pauseOverlay.enabled = false;
        GetChildWithName(GetChildWithName(this.gameObject, "PauseButton"), "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().SetText("| |");

        Time.timeScale = 1;
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void RespawnScreenOff()
    {
        Time.timeScale = 1;
        GetChildWithName(this.gameObject, "RestartOverlay").GetComponent<Canvas>().enabled = false;
        main.GetComponent<main_script>().respawn(main.GetComponent<main_script>().resp);
    }

    // Notifications
    // 0 = cancel
    // 1 = skip by ad
    // 2 = skip by ticket
    // 3 = last checkpoint
    //
    //
    //
    //
    //
    //
    //

    public void CancelNotification()
    {
        notificationOverlay.enabled = false;
    }
    public void Notification(int action)
    {
        GameObject note = GetChildWithName(notificationOverlay.gameObject, "Note");
        if(action == 1 || action == 2)
        {
            for (int j = 0; j < main.GetComponent<main_script>().checkpoints.Length; ++j)
            {
                var i = main.GetComponent<main_script>().checkpoints[j];
                if (i == main.GetComponent<main_script>().resp)
                {
                    if (j == main.GetComponent<main_script>().checkpoints.Length - 1)
                    {
                        action = 3;
                    }
                }
            }
        }
        if (action == 0)
        {

        }
        else if(action == 1)
        {
            GetChildWithName(note, "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().SetText(
                "You really want to watch an Ad to teleport to the next checkpoint?"
                );
            okAction = 1;
        }
        else if (action == 2)
        {
            GetChildWithName(note, "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().SetText(
                "You really want to spend 1 checkpoint skip ticket to teleport to the next checkpoint?"
            );
            okAction = 2;
        }
        else if(action == 3)
        {
            GetChildWithName(note, "Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().SetText(
                "You allready at the last checkpoint, just go to the portal)"
            );
            okAction = 0;
        }
        

        notificationOverlay.enabled = true;
    }
    public void OnOkClick()
    {
        notificationOverlay.enabled = false;
        if (okAction == 0)
        {
            CancelNotification();
        }else if(okAction == 1)
        {
            Debug.Log("AD");
            RespawnScreenOff();
            NextCheckpoint();
        }
    }
    public void NextCheckpoint()
    {
        GameObject newCheckpoint = null;
        for (int j = 0; j < main.GetComponent<main_script>().checkpoints.Length; ++j)
        {
            var i = main.GetComponent<main_script>().checkpoints[j];
            if (i == main.GetComponent<main_script>().resp)
            {
                newCheckpoint = main.GetComponent<main_script>().checkpoints[j + 1];
            }
        }
        Debug.Log(newCheckpoint);
        main.GetComponent<main_script>().respawn(newCheckpoint);
    }
}
