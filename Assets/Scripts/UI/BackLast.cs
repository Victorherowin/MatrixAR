using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackLast : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(BackLastMenu);
    }

    public void BackLastMenu()
    {
        SceneManager.LoadSceneAsync("001-menu");
    }
}
