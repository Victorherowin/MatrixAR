using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackA : Skill {
	public override float GetDamage(Weapon weapon,Entity player)
	{
		return weapon.ATK + player.ATK + Random.Range (-weapon.RandomRange, weapon.RandomRange) * weapon.RandomFactor+7.5f;
	}

	public override string Name {
		get {
			return "PlayerAttackA";
		}
	}
}
