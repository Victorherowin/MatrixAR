using UnityEngine;

using System.Collections;


public class EnemySpawn : MonoBehaviour {
    public GameObject prefab;
    public GameObject Spawn() {
        return GameObject.Instantiate(prefab, transform.position, transform.rotation) as GameObject;

    }

}
