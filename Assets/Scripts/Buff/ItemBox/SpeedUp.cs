using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Buff {
    public override string Name
    {
        get
        {
            return "SpeedUp";
        }
    }

    private float m_old_speed;

    public override void StartBuff(Entity entity)
    {
        RemainTime = 20.0f;
        var move = entity.GetComponent<PlayerMove>();
        m_old_speed = move.speed;
        move.speed *= 1.5f;
    }

    public override void DestroyBuff(Entity entity)
    {
        var move = entity.GetComponent<PlayerMove>();
        move.speed = m_old_speed;
    }
}
