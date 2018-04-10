using UnityEngine;
using System.Collections;

public class PlayerAnimationAttackalone : MonoBehaviour
{

    private Animator animator;
    private bool isCanAttackB;
    private TrailRenderer m_trail_renderer;

    // Use this for initialization
    void Start()
    {
        foreach (var t in GetComponentsInChildren<TrailRenderer>())
        {
            if (t.name == "SwordTrail")
                m_trail_renderer = t;
        }
        animator = GetComponent<Animator>();

        global::EventDelegate NormalAttackEvent = new global::EventDelegate(this, "OnNormalAttackClick");
        GameObject.Find("NormalAttack").GetComponent<UIButton>().onClick.Add(NormalAttackEvent);

        global::EventDelegate RangeAttackEvent = new global::EventDelegate(this, "OnRangeAttackClick");
        GameObject.Find("RangeAttack").GetComponent<UIButton>().onClick.Add(RangeAttackEvent);

        global::EventDelegate RedAttackEvent = new global::EventDelegate(this, "OnRedAttackClick");
        GameObject redAttack = GameObject.Find("RedAttack");
        redAttack.GetComponent<UIButton>().onClick.Add(RedAttackEvent);
        redAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var status = animator.GetCurrentAnimatorStateInfo(0);
        if (status.IsName("PlayerAttackA") || status.IsName("PlayerAttackB") || status.IsName("PlayerAttackRange"))
            m_trail_renderer.enabled = true;
        else
            m_trail_renderer.enabled = false;
    }


    public void OnNormalAttackClick()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackA") && isCanAttackB)
        {
            animator.SetTrigger("AttackB");
        }
        else
        {
            animator.SetTrigger("AttackA");
        }
    }
    public void OnRangeAttackClick()
    {
        animator.SetTrigger("AttackRange");
    }
    public void OnRedAttackClick()
    {
        animator.SetTrigger("AttackA");
    }

    public void AttackBEvent1()
    {
        isCanAttackB = true;
    }
    public void AttackBEvent2()
    {
        isCanAttackB = false;
    }

}
