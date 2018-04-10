using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scoreboard : NetworkBehaviour {
    [SyncVar]
    private bool m_scoring = false;

    public static Scoreboard Instance;

    void Awake () {
        if (Instance == null)
            Instance = this;
        else
            throw new UnityException("Scoreboard: Instance != null");
	}

    private Dictionary<Entity, int> m_killer_record = new Dictionary<Entity, int>();

    /// <summary>
    /// 提交被杀信息(Server)
    /// </summary>
    /// <param name="entity">攻击者</param>
    /// <param name="target">被攻击者</param>
    public void SubmitDeadPlayer(Entity entity,Entity target)
    {
        try
        {
            m_killer_record[entity]++;
        }
        catch (KeyNotFoundException)
        {
            m_killer_record[entity] = 0;
        }
        //下发杀敌消息到客户端
        RpcBroadcastDeadPlayer(entity.GetComponent<NetworkIdentity>(), target.GetComponent<NetworkIdentity>());
    }

    [ClientRpc]
    void RpcBroadcastDeadPlayer(NetworkIdentity entity_id, NetworkIdentity target)
    {
        var entity = entity_id.GetComponent<Entity>();
        try
        {
            m_killer_record[entity]++;
        }
        catch (KeyNotFoundException)
        {
            m_killer_record[entity] = 0;
        }
    }

    public int GetPlayerKillNumber(Entity e)
    {
        try
        {
            return m_killer_record[e];
        }
        catch (KeyNotFoundException)
        {
            return 0;
        }
    }

    public void Record()
    {
        m_scoring = true;
    }

    public void StopRecord()
    {
        m_scoring = false;
    }

    public bool IsRecording()
    {
        return m_scoring;
    }

    private void Update()
    {
        
    }
}
