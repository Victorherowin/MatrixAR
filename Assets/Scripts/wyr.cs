using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class wyr : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
       
    }
	
   public void TaskOnClick()
    {
		SceneManager.LoadSceneAsync("002-play");
    }
    // Update is called once per frame
    public void TaskOnClick1()
    {
		Application.Quit();
    }

	public void TaskOnClickMultiplay()
	{
		SceneManager.LoadSceneAsync("004-room");
	}
}
