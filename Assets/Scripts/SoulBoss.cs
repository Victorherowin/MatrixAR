using UnityEngine;
using System.Collections;

public class SoulBoss : MonoBehaviour {

    private Transform player;
    private CharacterController cc;
    private Animator animator;

    public float attackDistance = 3;//这个是攻击距离，也是寻路的目标距离
    public float speed = 2;
    public float attackTime = 3;
    private float attackTimer = 0;
    void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        cc = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        attackTimer = attackTime;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance <= attackDistance) {//在攻击距离之内
            attackTimer += Time.deltaTime;
            if (attackTimer > attackTime) {//达到计时的攻击时间
                int num = Random.Range(0, 2);
                if (num == 0) {
                    animator.SetTrigger("Attack1");
                } else {
                    animator.SetTrigger("Attack2");
                }
                attackTimer = 0;
            } else {
                animator.SetBool("Walk", false);
            }
        } else {//进行跟踪 跑向目标
            attackTimer = attackTime;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BossRun01")) {
                // cc.SimpleMove(transform.forward * speed);
                transform.Translate(Vector3.forward*speed);
            }
            animator.SetBool("Walk", true);
        }
	}
}

