using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemBox : NetworkBehaviour {

    Buffs.BuffIndex[] ITEM_BUFFS = new Buffs.BuffIndex[] {
        Buffs.BuffIndex.ContinueLossHP,
        Buffs.BuffIndex.LossHP,
        Buffs.BuffIndex.RestoreEnergy,
        Buffs.BuffIndex.RestoreHP,
        Buffs.BuffIndex.SpeedUp,
        Buffs.BuffIndex.AdditionalDamage
    };

    public float RotateSleep = 30.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, RotateSleep * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;

        int i = 0;
        try
        {
            Player p = other.GetComponentInParent<Player>();
            i = Random.Range(0, ITEM_BUFFS.Length);
            p.AddBuff(p, ITEM_BUFFS[i]);
            ItemSpawner.Instance.Destroy(this);
        }
        catch (System.Exception e)
        {
            //Debug.unityLogger.Log("ItemBuffs:" + i.ToString());
           // Debug.unityLogger.Log(ITEM_BUFFS[i].ToString());
            throw e;
        }
    }
}
