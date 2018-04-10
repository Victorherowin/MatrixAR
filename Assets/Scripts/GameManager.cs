using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


	
public class GameManager : MonoBehaviour
{
    //private object PanelPopup;

    public GameObject PanelPopup;
    private void Start()
    {
        hide();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

public void GoToMainMenu()
   {
     Invoke("MainMenu", 1f);
  }

    public  void MainMenu()
    {
        // Application.LoadLevel("1MainMenu");
        SceneManager.LoadSceneAsync("001-menu");
    }

    public void GoToMainGame()
  {
       Invoke("MainGame", 1f);
    }

    public void MainGame()
    {
        // Application.LoadLevel("2MainGame");
        SceneManager.LoadSceneAsync("002-play");
    }

    public void hide()
    {
        foreach (var com in PanelPopup.GetComponentsInChildren<MonoBehaviour>())
            com.enabled = false;
    }

    public void show()
    {
        foreach (var com in PanelPopup.GetComponentsInChildren<MonoBehaviour>())
            com.enabled = true;
        //PanelPopop.SetActive(true);
    }
}


