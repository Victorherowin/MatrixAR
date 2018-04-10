using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalDamage : Buff {
    public override string Name
    {
        get
        {
            return "AdditionalDamage";
        }
    }

    private float old_atk;

    // Use this for initialization
    public override void StartBuff(Entity entity)
    {
        RemainTime = 12.0f;
        old_atk = entity.ATK;
        entity.ATK *= 2.0f;
    }

    public override void DestroyBuff(Entity entity)
    {
        entity.ATK = old_atk;
    }
}
