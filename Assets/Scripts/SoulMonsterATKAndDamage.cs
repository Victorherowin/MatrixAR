using UnityEngine;
using System.Collections;

public class SoulMonsterATKAndDamage : ATKAndDamage {

    private Transform player;
    void Awake() {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    public void MonAttack() {
        if (Vector3.Distance(transform.position, player.position) < attackDistance) {
            player.GetComponent<ATKAndDamage>().TakeDamage(normalAttack);
        }
    }

}
