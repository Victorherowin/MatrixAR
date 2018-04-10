using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerAnimationAttack : NetworkBehaviour
{

    private NetworkAnimator animator;
    private bool isCanAttackB;
	private TrailRenderer m_trail_renderer;

    // Use this for initialization
    public void Start()
    {
		foreach (var t in GetComponentsInChildren<TrailRenderer>()) {
			if (t.name == "SwordTrail")
				m_trail_renderer = t;
		}
		animator = GetComponent<NetworkAnimator>();

        if (!isLocalPlayer) return;

        global::EventDelegate NormalAttackEvent = new global::EventDelegate(this, "OnNormalAttackClick");
        GameObject.Find("NormalAttack").GetComponent<UIButton>().onClick.Add(NormalAttackEvent);

        global::EventDelegate RangeAttackEvent = new global::EventDelegate(this, "OnSpecialAttackClick");
        GameObject.Find("SpecialAttack").GetComponent<UIButton>().onClick.Add(RangeAttackEvent);

        global::EventDelegate RedAttackEvent = new global::EventDelegate(this, "OnRedAttackClick");
        GameObject redAttack = GameObject.Find("RedAttack");
        redAttack.GetComponent<UIButton>().onClick.Add(RedAttackEvent);
        redAttack.SetActive(false);

        BindSkill("SpecialAttack", "AttackRange", Skills.PlayerAttackRange);
    }

    // Update is called once per frame
    void Update()
    {
		var status = animator.animator.GetCurrentAnimatorStateInfo (0);
		if (status.IsName ("PlayerAttackA")||status.IsName("PlayerAttackB")||status.IsName("PlayerAttackRange"))
			m_trail_renderer.enabled = true;
		else
			m_trail_renderer.enabled = false;
    }

    public void OnNormalAttackClick()
    {
        //Debug.unityLogger.Log("N");
		if (animator.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackA") && isCanAttackB)
        {
            animator.SetTrigger("AttackB");
        } else {
            animator.SetTrigger("AttackA");
        }
    }

    public void OnSpecialAttackClick()
    {
        var state = animator.animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("PlayerStand") || state.IsName("PlayerRun"))
        {
            var mapping = skill_mapping["SpecialAttack"];
            var player = GetComponent<Player>();
            if (player.CurrentEnergy >= mapping.Value.NeedEnergy)
            {
                animator.SetTrigger(mapping.Key);
                player.CurrentEnergy -= mapping.Value.NeedEnergy;
            }
        }
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

    private Dictionary<string, KeyValuePair<string,Skill>> skill_mapping=new Dictionary<string, KeyValuePair<string, Skill>>();

    /// <summary>
    /// 绑定技能到按钮
    /// </summary>
    /// <param name="attack_button">按钮名(NormalAttack/SpecialAttack).</param>
    /// <param name="trigger">触发字符串.</param>
    /// <param name="skill">技能.</param>
    [Client]
    public void BindSkill(string attack_button,string trigger, Skill skill)
    {
        skill_mapping[attack_button] = new KeyValuePair<string,Skill>(trigger,skill);
    }

}
