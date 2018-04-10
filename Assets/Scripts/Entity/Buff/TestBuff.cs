using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuff : Buff {
	public override string Name
	{
		get{
			return "TestBuff";
		}
	}

	private float m_counter=0.0f;

    public override void StartBuff(Entity entity)
    {
        RemainTime = 3.0f;
    }

    public override void ApplyBuff (Entity entity)
	{
		if (m_counter >= 1.0f) {
			entity.TakeDamageWithoutInvincible (1.0f);
			m_counter = 0.0f;
		}
		
		m_counter += Time.fixedDeltaTime;
	}
}
