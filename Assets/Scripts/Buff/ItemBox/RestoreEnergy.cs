using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreEnergy : Buff {
    public override string Name
    {
        get
        {
            return "RestoreEnergy";
        }
    }

    public override void StartBuff(Entity entity)
    {
        entity.CurrentEnergy = Mathf.Min(entity.MAX_ENERGY, entity.CurrentEnergy + 50);
    }
}
