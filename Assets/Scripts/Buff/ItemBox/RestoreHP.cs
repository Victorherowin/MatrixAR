using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHP : Buff {
    public override string Name
    {
        get
        {
            return "RestoreHP";
        }
    }

    public override void StartBuff(Entity entity)
    {
        entity.CurrentHP = Mathf.Min(entity.MAX_HP, entity.CurrentHP + 50);
    }
}
