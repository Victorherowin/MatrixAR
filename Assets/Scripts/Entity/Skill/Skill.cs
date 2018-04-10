using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 技能类基类
/// </summary>
public abstract class Skill:NetworkBehaviour{
    /// <summary>
    /// 精力消耗
    /// </summary>
    public virtual int NeedEnergy
    {
        get
        {
            return 0;
        }
    }

    /// <summary>
    /// 冷却时间
    /// </summary>
    public float CooldownTime=0.0f;
	/// <summary>
	/// 剩余冷却时间
	/// </summary>
	[SyncVar]public float RemainTime=0.0f;

	public void Attack(Weapon weapon,Entity player,Entity target)
	{
        target.TakeDamage(player,GetDamage (weapon, player));
		AttachBuff (player,target);
	}

	/// <summary>
	/// 技能伤害，用于给子类计算技能伤害
	/// </summary>
	/// <returns>伤害值</returns>
	/// <param name="weapon">武器对象.</param>
	/// <param name="entity">玩家自己.</param>
	public abstract float GetDamage (Weapon weapon,Entity player);

    /// <summary>
    /// 给Entity加技能Buff
    /// </summary>
    /// <param name="entity">Buff施加者</param>
    /// <param name="target">被施加Buff的Entity.</param>
    public virtual void AttachBuff(Entity entity,Entity target){}

	/// <summary>
	/// 要求与Animator中的动作名一致
	/// </summary>
	/// <value>技能名</value>
	public abstract string Name{ get;}

}