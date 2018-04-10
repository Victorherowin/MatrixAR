using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawner : NetworkBehaviour {

    public static ItemSpawner Instance=null;
    public int MaxAliveItemBox = 5;
    private ItemBox m_itembox_prefab;
    private int m_alive_item_box = 0;
	// Use this for initialization
	void Start () {
        if (!isServer) return;
        if (Instance == null) Instance = this;
        else throw new UnityException("ItemSpawner.Instance!=null");
        
        m_itembox_prefab = Resources.Load<ItemBox>("Prefab/ItemBox/ItemBox");

        Invoke("SpawnItemBox", 15.0f);
        Invoke("SpawnItemBox", 30.0f);
        Invoke("SpawnItemBox", 45.0f);
        Invoke("SpawnItemBox", 60.0f);

        Invoke("SpawnItemBox", 75.0f);
        Invoke("SpawnItemBox", 90.0f);
        Invoke("SpawnItemBox", 105.0f);
        Invoke("SpawnItemBox", 75.0f);
        Invoke("SpawnItemBox", 90.0f);
        Invoke("SpawnItemBox", 105.0f);
    }

    void SpawnItemBox()
    {
        if(m_alive_item_box<5)
        { 
            var wp = PlayerManager.Instance.GetWeaknessPlayer();
            if (wp == null) return;

            Vector3 o = wp.transform.position;
            var pos = o + new Vector3(Random.Range(-5.0f,5.0f),0.0f, Random.Range(-5.0f, 5.0f));
            var item=Instantiate(m_itembox_prefab, pos,Quaternion.identity);
            NetworkServer.Spawn(item.gameObject);
            m_alive_item_box++;
        }
    }

    public void Destroy(ItemBox b)
    {
        NetworkServer.Destroy(b.gameObject);
        m_alive_item_box--;
    }
}
