using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueLossHP : Buff {
    public override string Name
    {
        get
        {
            return "ContinueLossHP";
        }
    }

    public override void StartBuff(Entity entity)
    {
        RemainTime = 12.0f;
    }

    private float m_time = 0.0f;

    public override void ApplyBuff(Entity entity)
    {
        if (m_time >= 1.0f)
        {
            entity.TakeDamageWithoutInvincible(5.0f);
            m_time = 0.0f;
        }
        m_time += Time.fixedDeltaTime;
    }
}
