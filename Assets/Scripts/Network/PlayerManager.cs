using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

///Server Only
public class PlayerManager : NetworkBehaviour {

    public static PlayerManager Instance;
    // Use this for initialization
    void Start () {
        if (Instance == null) Instance = this;
        else throw new UnityException("PlayerManager.Instance!=null");
    }

    private List<Player> m_player_list=new List<Player>();
    public List<Player> RoleList
    {
        get
        {
            return m_player_list;
        }
    }

    public void RegisterPlayer(Player player)
    {
        m_player_list.Add(player);
    }

    public void UnregisterPlayer(Player player)
    {
        m_player_list.Remove(player);
    }

    public Player GetWeaknessPlayer()
    {
        if (m_player_list.Count == 0) return null;

        Player wp= m_player_list[0];
        foreach(var p in m_player_list)
        {
            if (p.CurrentHP < wp.CurrentHP)
                wp = p;
        }

        return wp;
    }

    public Player GetSupremacyPlayer()
    {
        if (m_player_list.Count == 0) return null;

        Player sp = m_player_list[0];

        bool f = false;
        foreach (var p in m_player_list)
        {
            if (p.CurrentHP != sp.CurrentHP)
                f = true;
            if (p.CurrentHP > sp.CurrentHP)
                sp = p;
        }

        if (f)
            return sp;
        return null;
    }
}
