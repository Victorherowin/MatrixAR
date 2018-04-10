using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Buffs{
	/// <summary>
	/// 与Buff类名保持一致
	/// </summary>
	public enum BuffIndex
	{
        TestBuff =0,
        RestoreHP=1,
        RestoreEnergy=2,
        SpeedUp=3,
        AdditionalDamage=4,
        LossHP=5,
        ContinueLossHP=6
    }

	public static Buff GetBuff(BuffIndex index)
	{
		return System.Activator.CreateInstance(Type.GetType(index.ToString()))as Buff;
	}
}
