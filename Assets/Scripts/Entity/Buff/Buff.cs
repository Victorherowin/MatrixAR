using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Buff:NetworkBehaviour {
	/// <summary>
	/// Buff剩余时间。默认无限时间	
	/// </summary>
	[SyncVar]public float RemainTime = float.NegativeInfinity;
    /// <summary>
    /// Buff的施加者
    /// </summary>
    public Entity Entity{ get; set; }


	/// <summary>
	/// Buff名
	/// </summary>
	/// <value>Buff名</value>
	public abstract string Name{ get;}

    public virtual void StartBuff(Entity entity) { }
    public virtual void DestroyBuff(Entity entity) { }

    /// <summary>
    /// 持续应用Buff效果。
    /// </summary>
    /// <param name="entity">被施加者的Entity.</param>
    public virtual void ApplyBuff (Entity entity){}

    public void UpdateRemainTime()
    {
        if (RemainTime > 0.0f)
            RemainTime -= Time.fixedDeltaTime;
    }
}
