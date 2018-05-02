using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class enemySet{
	public int[] monsterNumArr = new int[7];
}

public class EnemyManager : MonoBehaviour {
	GameObject player;
	//레벨당 등장하는 적의 수를 저장하는 클래스 배열
	public enemySet[] levelForEnemyNum;
	//현재 stage 레벨 저장
	int nextLevel = 1;
	int currScore = 0;
	bool isBoss = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		objPooling.objpooling.objectPoolFunc (levelForEnemyNum[0]);
	}

	void Update () {
			if (ScoreManager.score >= 500 && nextLevel < levelForEnemyNum.Length
			   && currScore != ScoreManager.score && currScore / 500 != ScoreManager.score / 500) {
				if (ScoreManager.score <= 5590) {
					currScore = ScoreManager.score;
					nextLevel = currScore / 500;
					StartCoroutine ("monsterSpawn");
				}
			}

		Vector3 randomPos = new Vector3 (Random.Range (-50f, 50f), 0.5f, Random.Range (-50f, 50f));
		if ((player.transform.position - randomPos).sqrMagnitude <= 1000.0f) {
			randomPos = new Vector3 (Random.Range (-50f, 50f), 0.5f, Random.Range (-50f, 50f));
		} else {
			GameObject enemyObj = objPooling.objpooling.getPooledObj ();
			if (enemyObj == null)
				return;
			Vector3 finalPos;
			if (isBoss) {
				finalPos = new Vector3 (10.0f, enemyObj.transform.position.y, 10.0f);
			} else {
				finalPos = randomPos;
			}
			finalPos.y = enemyObj.transform.position.y;
			enemyObj.transform.position = finalPos;
			enemyObj.GetComponent<BasicEnemyMovement> ().hpReset ();
			enemyObj.SetActive (true);
		}
	}

	public void CheckLevel(int nextLevel){
		if (nextLevel == 5 || nextLevel == 10) {
			isBoss = true;
			objPooling.objpooling.clearPool ();
			objPooling.objpooling.objectPoolFunc (levelForEnemyNum [nextLevel]);
		}
		else {
			if (nextLevel == 6) {
				isBoss = false;
				objPooling.objpooling.clearPool ();
			} else if (nextLevel == 11) {
				isBoss = false;
			}
			objPooling.objpooling.objectPoolFunc (levelForEnemyNum [nextLevel]);
		}
	}

	IEnumerator monsterSpawn(){
		yield return new WaitForSeconds (1.5f);
		CheckLevel (nextLevel);
	}
}
