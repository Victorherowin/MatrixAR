using UnityEngine;

using System.Collections;


public class UIAttack : MonoBehaviour {

    public static UIAttack _instance;
    public GameObject normalAttack;
    public GameObject rangeAttack;
    public GameObject redAttack;
    void Awake() {
        _instance = this;
    }

	// Use this for initialization
	
void Start () {
        normalAttack = transform.Find("NormalAttack").gameObject;
        rangeAttack = transform.Find("RangeAttack").gameObject;
        redAttack = transform.Find("RedAttack").gameObject;
	}

    public void TurnToOneAttack() {
        normalAttack.SetActive(false);
        rangeAttack.SetActive(false);
        redAttack.SetActive(true);
    }

    public void TurnToTwoAttack() {
        normalAttack.SetActive(true);
        rangeAttack.SetActive(true);
        redAttack.SetActive(false);
    }
	
}
