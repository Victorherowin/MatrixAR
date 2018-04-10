using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalMenu : MonoBehaviour {
    public UIButton ButtonMainMenu;
    public UIButton ButtonQuit;
    public UIButton ButtonReplay;
    // Use this for initialization
    void Start () {

        ButtonMainMenu.onClick.Add(new EventDelegate(OnClick_ButtonMainMenu));
        ButtonQuit.onClick.Add(new EventDelegate(OnClick_ButtonQuit));
        ButtonReplay.onClick.Add(new EventDelegate(OnClick_ButtonReplay));
        Hide();
    }

    void OnClick_ButtonMainMenu()
    {
        SceneManager.LoadSceneAsync("001-menu");
    }

    void OnClick_ButtonQuit()
    {
#if !UNITY_IOS
        Application.Quit();
#endif
    }

    void OnClick_ButtonReplay()
    {
        SceneManager.LoadSceneAsync("002-network");
    }

    public void Show()
    {
        foreach(var com in GetComponentsInChildren<MonoBehaviour>())
        {
            com.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (var com in GetComponentsInChildren<MonoBehaviour>())
        {
            com.enabled = false;
        }
    }
}
