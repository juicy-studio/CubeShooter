using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objPooling : MonoBehaviour {

	public static objPooling objpooling;

	public GameObject[] enemyType;
	public GameObject play_Objs;
	public List<GameObject> enemyPool;
	GameObject bulletHolder; //npc가 생성하는 총알을 담는 오브젝트


	void Awake() {
		enemyPool = new List<GameObject> ();
		objpooling = this;
		bulletHolder = GameObject.FindGameObjectWithTag("BulletPool");
	}

	//오브젝트를 생성하고 부모 오브젝트에 연결하여 풀에 넣어준다.
	public void objectPoolFunc(enemySet enemy){
		for(int i = 0;i<enemy.monsterNumArr.Length;i++){
			for(int j = 0;j<enemy.monsterNumArr[i];j++){
				GameObject melee_Enemy = Instantiate (enemyType [i]);
				melee_Enemy.transform.parent = play_Objs.transform;
				melee_Enemy.SetActive (false);
				enemyPool.Add (melee_Enemy);
			}
		}
	}

	//풀에 있는 오브젝트를 꺼내어 활성화 시킨다.
	public GameObject getPooledObj(){
		for (int i = 0; i < enemyPool.Count; i++) {
			if (!enemyPool [i].activeInHierarchy) {
				return enemyPool [i];
			}
		}

		return null;
	}

	//보스전 전,후로 enemypool의 모든 오브젝트를 초기화함
	public void clearPool(){
		for (int i = 0; i < enemyPool.Count; i++) {
				Destroy (enemyPool [i]);
		}
		enemyPool.Clear ();

		Transform[] trans = bulletHolder.GetComponentsInChildren<Transform>(); 
		foreach (Transform tr in trans) 
		{ 
			if (tr.CompareTag ("EnemyBullet")) {
				Destroy (tr.gameObject); 
			}
		}
	}
}
