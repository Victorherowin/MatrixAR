using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayGameManager : NetworkBehaviour {

    public enum GameStatuses
    {
        Begin, Playing,End
    }

    public static MultiplayGameManager Instance;
    private FinalMenu m_final_menu;
    public FinalMenu FinalMenu { get { return m_final_menu; } }
    public Player Winner = null;
    [SyncVar]
    private GameStatuses m_game_status=GameStatuses.Begin;
    public GameStatuses GameStatus
    {
        get
        {
            return m_game_status;
        }
    }

    [SyncVar]
    private int m_survival_number = 0;
    public int SurvivalNumber
    {
        get
        {
            return m_survival_number;
        }
        set
        {
            m_survival_number = value;
            if (m_survival_number >= 2)
                GameStart();
        }
    }

    // Use this for initialization
    private void Start()
    {
        if (Instance == null) Instance = this;
        else throw new UnityException("MultiplayGameManager.Instance!=null");
    }
    public override void OnStartClient()
    {
        m_final_menu = GameObject.Find("PanelPopup").GetComponent<FinalMenu>();
        base.OnStartClient();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStatus==GameStatuses.Begin) return;

        if (GameStatus==GameStatuses.Playing && isServer)
        {
            if (m_survival_number <= 1)
            {
                GameEnd(PlayerManager.Instance.RoleList[0]);
            }
        }
    }

    [Server]
    private void GameEnd(Player winner)
    {
        Winner = winner;
        RpcGameEnd(winner.GetComponent<NetworkIdentity>());
        if(isClient&&isServer)//is Host
        {
            if (winner == null)
            {
                //Debug.unityLogger.Log("play even");
                return;
            }

            Player p = winner;
            m_final_menu.Show();
            if (Player.LocalPlayerInstance == p)
                BgmManager.Instance.PlayVictoryBgm();
            else
                BgmManager.Instance.PlayFailedBgm();
        }
        m_game_status = GameStatuses.End;
        CancelInvoke();
    }

    [ClientRpc]
    void RpcGameEnd(NetworkIdentity id)
    {
        if(id==null)
        {
            //Debug.unityLogger.Log("play even");
            return;
        }

        Winner = id.GetComponent<Player>();

        Player p = id.GetComponent<Player>();
        m_final_menu.Show();
        if (Player.LocalPlayerInstance == p)
            BgmManager.Instance.PlayVictoryBgm();
        else
            BgmManager.Instance.PlayFailedBgm();
    }

    [SyncVar] private int m_time = 120;
    public int Time { get { return m_time; } }

    [Server]
    public void GameStart()
    {
        if (GameStatus==GameStatuses.Playing) return;

        var spawner=GameObject.Instantiate(Resources.Load("Prefab/ItemBox/ItmeSpawner"))as GameObject;
        NetworkServer.Spawn(spawner);
        InvokeRepeating("DecrementTime", 1.0f, 1.0f);
        m_game_status = GameStatuses.Playing;
    }

    private void DecrementTime()
    {
        m_time--;
        if (m_time == 0)
        {
            GameEnd(PlayerManager.Instance.GetSupremacyPlayer());
        }
    }

    private void OnGUI()
    {
        if(GameStatus==GameStatuses.End)
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            if (Winner!=null)
                GUI.Label(new Rect(Screen.width / 2 - 50, 100, 100, 50), "胜者:"+Winner.name);
            else
                GUI.Label(new Rect(Screen.width / 2 - 50, 100, 100, 50), "平局");
        }
    }

}
