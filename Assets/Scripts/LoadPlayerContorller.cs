using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Networking;

public class LoadPlayerContorller : MonoBehaviour,ITrackableEventHandler {

	public GameObject EffectPrefab;
	public float EndTime;
	public GameObject PlayerPrefab;

	GameObject active_effect_object=null;

	static bool is_first_time=true;

    // Use this for initialization
    void Start () {
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	void LoadEffect()
	{
		active_effect_object=Instantiate (EffectPrefab, transform);
		active_effect_object.transform.localPosition = new Vector3 (0,0,0);
		active_effect_object.transform.localScale = new Vector3 (1,1,1);
		Destroy (active_effect_object,EndTime);
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			if (is_first_time) {
				PlayerSpawnMessage msg = new PlayerSpawnMessage ();

				msg.PrefabID = GetComponent<BindModelPrefab>().PrefabID;
                msg.PlayerName = ArCard.GlobalUserInfo.UserName;

                ClientScene.AddPlayer (NetworkClient.allClients[0].connection,0,msg);

				GameObject obj=Instantiate (EffectPrefab, transform);
				obj.transform.localPosition = new Vector3 (0,0,0);
				obj.transform.localScale = new Vector3 (1,1,1);
				Destroy (obj, EndTime);

				is_first_time = false;
			}
		}
	}
}
