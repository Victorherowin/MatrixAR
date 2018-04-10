using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public float hp = 100;
    public float normalAttack = 20;
    public float attackDistance = 1;
    protected Animator animator;
    public float attackB = 80;
    public float attackRange = 100;
    public float attackGun = 100;
    public AudioClip shotClip;
    public AudioClip swordClip;
    public GameObject player1;
    //Attack a = new Attack();
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
        }
        else
        {
            animator.SetBool("Dead", true);
        }
    }
    public void AttackA()
    {
        AudioSource.PlayClipAtPoint(swordClip, transform.position, 1f);
        float distance = attackDistance;
        GameObject enemy = player1;
        if (enemy!=null)
        {
            Vector3 targetPos = enemy.transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
            TakeDamage(normalAttack);
        }
    }
    public void AttackB()
    {
        AudioSource.PlayClipAtPoint(swordClip, transform.position, 1f);
        float distance = attackDistance;
        GameObject enemy = player1;
        if (enemy != null)
        {
            Vector3 targetPos = enemy.transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
            TakeDamage(normalAttack);
        }
    }
    public void AttackRange()
    {
        AudioSource.PlayClipAtPoint(swordClip, transform.position, 1f);
        float temp = Vector3.Distance(player1.transform.position, transform.position);
        if (temp < attackDistance)
        {
            TakeDamage(attackRange);
        }
    }
    // Use this for initialization
}
