using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawnMessage : MessageBase {

	public int PrefabID;
    public string PlayerName;

	public override void Deserialize(NetworkReader reader)
	{
		PrefabID = reader.ReadInt32();
	}


	// This method would be generated
	public override void Serialize(NetworkWriter writer)
	{
		writer.Write (PrefabID);
	}
}
