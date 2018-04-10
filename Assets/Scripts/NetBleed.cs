using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetBleed : MonoBehaviour {

	public GameObject player=null;
	private float bleed=1000;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {
			GameObject[] gbs = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject gb in gbs) {
				if (gb.GetComponent<PlayerAnimationAttack> ().isLocalPlayer) {
					player = gb;
					break;
				}
			}
		} else {
			bleed = player.GetComponent<PlayerATKAndDamage> ().hp;
			GetComponent<RectTransform>().sizeDelta = new Vector2(bleed/1000*500, 60f);
			GetComponent<RectTransform>().anchoredPosition = new Vector2((bleed/1000f*500f)/2f+(1-bleed/1000f)*20f-10,-10f);
		}
	}
}
