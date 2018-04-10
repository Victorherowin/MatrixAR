using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttackRange : Skill {
    public override int NeedEnergy
    {
        get
        {
            return 1;
        }
    }

    public override float GetDamage(Weapon weapon,Entity player)
	{
		return (weapon.ATK + player.ATK + Random.Range (-weapon.RandomRange, weapon.RandomRange) * weapon.RandomFactor)+17.5f;
	}

	/*public override void AttachBuff(Entity entity,Entity target)
	{
		target.AddBuff (entity,Buffs.BuffIndex.TestBuff);
	}*/

	public override string Name {
		get {
			return "PlayerAttackRange";
		}
	}
}
