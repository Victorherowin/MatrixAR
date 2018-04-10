using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTimer : NetworkBehaviour {

    [SyncVar] public float Time=120.0f;

	// Use this for initialization
	void Start () {
        if (!isServer) return;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isServer) return;

        if (Time > 0)
            Time -= UnityEngine.Time.deltaTime;
	}
}
