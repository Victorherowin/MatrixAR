using UnityEngine;
using System.Collections;

public class PlayerAward : MonoBehaviour {

    public GameObject singleSwordGo;
    public GameObject dualSwordGo;
    public GameObject gunGo;
    public float exitTime = 10;//奖励武器存在的时间
    public float dualSwordTimer = 0;
    public float gunTimer = 0;

    void Update() {
        if (dualSwordTimer > 0) {
            dualSwordTimer -= Time.deltaTime;
            if (dualSwordTimer < 0) {
                TurnToSingleSword();
            }
        }

        if (gunTimer > 0) {
            gunTimer -= Time.deltaTime;
            if (gunTimer < 0) {
                TurnToSingleSword();
            }
        }
    }

    public void GetAward(AwardType type) {
        if (type == AwardType.DualSword) {
            TurnToDualSword();
        } else if (type == AwardType.Gun) {
            TurnToGun();
        }
    }

    void TurnToDualSword() {
        singleSwordGo.SetActive(false);
        dualSwordGo.SetActive(true);
        gunGo.SetActive(false);
        dualSwordTimer = exitTime;
        gunTimer = 0;
        UIAttack._instance.TurnToTwoAttack();
    }
    void TurnToGun()
    {
        singleSwordGo.SetActive(false);
        dualSwordGo.SetActive(false);
        gunGo.SetActive(true);
        dualSwordTimer = exitTime;
        gunTimer = 0;
        UIAttack._instance.TurnToTwoAttack();
    }
    //void TurnToGun() {
    //    singleSwordGo.SetActive(false);
    //    dualSwordGo.SetActive(false);
    //    gunGo.SetActive(true);
    //    gunTimer = exitTime;
    //    dualSwordTimer = 0;
    //    UIAttack._instance.TurnToOneAttack();
    //}

    void TurnToSingleSword() {
        singleSwordGo.SetActive(true);
        dualSwordGo.SetActive(false);
        gunGo.SetActive(false);
        dualSwordTimer = 0;
        gunTimer = 0;
        UIAttack._instance.TurnToTwoAttack();
    }
}