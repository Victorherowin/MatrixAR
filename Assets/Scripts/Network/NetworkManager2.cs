using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager2 : NetworkManager {
	public List<BindModelPrefab> RegisteredPlayerBindPoint;

	private static NetworkManager2 _instance=null;
	public static NetworkManager2 Instance{get{return _instance; }}

    public int RoomPlayerNum=0;
	private Broadcast.Server m_broadcast_server;

	public void Start()
	{
		if (_instance == null)
			_instance = this;
		else
			throw new UnityException ("重复的NetworkManager2实例");

		if (JoinServerArgs.isCreateHost) {
			m_broadcast_server = new Broadcast.Server ();
			m_broadcast_server.ServerName = JoinServerArgs.ServerName;
			StartHost ();
		} else {
			networkAddress = JoinServerArgs.ServerIP;
			StartClient ();
		}
	}

	void OnDestroy()
	{
		m_broadcast_server.Destroy ();
	}

    private int m_role_num=0;


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId,NetworkReader extraMessageReader)
	{
		PlayerSpawnMessage msg=new PlayerSpawnMessage();
		msg.Deserialize (extraMessageReader);

		BindModelPrefab player_prefab = null;

		foreach (var prefab in RegisteredPlayerBindPoint) {
			if (prefab.PrefabID == msg.PrefabID) {
				player_prefab = prefab;
				break;
			}
		}

		if (player_prefab == null)
			throw new PlayerPrefsException ("没有没有在注册表中找到PrefabID:"+msg.PrefabID);

		GameObject obj=Instantiate (player_prefab.PlyerPrefab,player_prefab.transform);
        obj.name = obj.name.Replace("(Clone)","");
        obj.AddComponent<RegisterPlayer>();
        obj.GetComponent<Player>().PlayerName = msg.PlayerName;
		obj.transform.localPosition = new Vector3 (0,0,0);
		obj.transform.localScale = new Vector3 (1,1,1);

		NetworkServer.AddPlayerForConnection (conn, obj, playerControllerId);

        MultiplayGameManager.Instance.SurvivalNumber++;
    }

    bool first_connection = true;

	public override void OnServerConnect(NetworkConnection conn)
	{
        if(first_connection)
        {
            //Create Scoreboard
            GameObject obj = Instantiate(spawnPrefabs[0]);
            obj.name = "Scoreboard";
            NetworkServer.Spawn(obj);


            //Create GameManager
            obj = Instantiate(spawnPrefabs[1]);
            obj.name = "MultiplayGameManager";
            NetworkServer.Spawn(obj);

            first_connection = false;
        }
        RoomPlayerNum++;
		base.OnServerConnect (conn);
	}
		
	public override void OnServerDisconnect(NetworkConnection conn)
	{
        MultiplayGameManager.Instance.SurvivalNumber--;
        RoomPlayerNum--;
		base.OnServerDisconnect (conn);
	}

    bool is_disconntect = false;
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        MultiplayGameManager.Instance.FinalMenu.Show();
        is_disconntect = true;
        base.OnClientDisconnect(conn);
    }

    private void OnGUI()
    {
        if (is_disconntect)
            GUI.TextArea(new Rect(500, 600, 200, 20),"连接丢失!");
    }
}
