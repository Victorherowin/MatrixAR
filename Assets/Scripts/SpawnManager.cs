using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager _instance;
    
    public EnemySpawn[] monsterSpawnArray;
    public EnemySpawn[] bossSpawnArray;

    public List<GameObject> enemyList = new List<GameObject>();
    public bool Last = false;
    
    //public AudioClip victoryClip;
    void Awake() {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn() {
        //第一波敌人的生成
        foreach (EnemySpawn s in monsterSpawnArray) {
            enemyList.Add(s.Spawn());
        }

        while(enemyList.Count>0){
            yield return new WaitForSeconds(3f);//暂停1.2f
            print(Time.time);
        }

        //第二波敌人的产生
        foreach (EnemySpawn s in monsterSpawnArray) {
            enemyList.Add(s.Spawn());
        }
        yield return new WaitForSeconds(5f);
        foreach (EnemySpawn s in monsterSpawnArray) {
            enemyList.Add(s.Spawn());
        }

         while(enemyList.Count>0){
            yield return new WaitForSeconds(1.2f);
            print(Time.time);
        }

        //第三波敌人的产生
        foreach (EnemySpawn s in monsterSpawnArray) {
            enemyList.Add(s.Spawn());
        }
        yield return new WaitForSeconds(3f);
        foreach (EnemySpawn s in monsterSpawnArray) {
            enemyList.Add(s.Spawn());
        }
        yield return new WaitForSeconds(3f);
        foreach(EnemySpawn s in bossSpawnArray ){
            enemyList.Add(s.Spawn());
        }
        Last = true;
        while (enemyList.Count > 0) {
            yield return new WaitForSeconds(0.2f);
        }
        //游戏胜利
        //AudioSource.PlayClipAtPoint(victoryClip, transform.position, 1f);
    }

}
