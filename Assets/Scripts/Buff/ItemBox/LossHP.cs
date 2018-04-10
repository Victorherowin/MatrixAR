using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossHP : Buff {
    public override string Name
    {
        get
        {
            return "LossHP";
        }
    }

    public override void StartBuff(Entity entity)
    {
        entity.CurrentHP -= 40.0f;
    }
}
