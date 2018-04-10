using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BleedUi : MonoBehaviour {
	public GameObject player;
	private float bleed;
	public int defaultTime=75;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		bleed = player.GetComponent<PlayerATKAndDamage> ().hp;
		GetComponent<RectTransform>().sizeDelta = new Vector2(bleed/1000*500, 60f);
		GetComponent<RectTransform>().anchoredPosition = new Vector2((bleed/1000f*500f)/2f+(1-bleed/1000f)*20f-10,-10f);
	}


}
