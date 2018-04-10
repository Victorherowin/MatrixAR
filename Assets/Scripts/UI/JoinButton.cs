using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinButton : MonoBehaviour {
	public int ID;

	void SelectButton()
	{
		GetComponentInParent<UI.RoomButton> ().JoinServer (ID);
	}

	void Start()
	{
		GetComponent<Button> ().onClick.AddListener(SelectButton);
	}
}
