using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChange : MonoBehaviour {

	private float Speed;
	private float changeSpeed;
	int currentStage;
	public Material[] TileColor;
	MeshRenderer meshRender;
	Material currentMaterial;
	float randNum;
	float randSpeed;
	int Cycle=0;

	// Use this for initialization
	void Start()
	{
		currentStage = ScoreManager.stage;
		meshRender = GetComponent<MeshRenderer> ();

		randNum = Random.Range (0.2f, 0.8f);
		randSpeed = 1.0f - randNum;
	}

	// Update is called once per frame
	void Update()
	{

		if (currentStage < ScoreManager.stage) {

			if (currentStage == 0+8*Cycle) {
				StartCoroutine	(ChangeColor (TileColor [0], TileColor [2]));
			} else if (currentStage == 1+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine	(ChangeColor (TileColor [2], TileColor [3]));
			} else if (currentStage == 2+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor (TileColor [3], TileColor [1]));
			} else if (currentStage == 3+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine	(ChangeColor (TileColor [1], TileColor [4]));
			} else if (currentStage == 4+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor (TileColor [4], TileColor [6]));
			} else if (currentStage == 5+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine	(ChangeColor (TileColor [6], TileColor [5]));
			} else if (currentStage == 6+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor (TileColor [5], TileColor [1]));
			} else if (currentStage == 7+8*Cycle) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor (TileColor [1], TileColor [0]));
				Cycle++;
			}

			currentStage = ScoreManager.stage;
		}


		/*
		if (currentStage < ScoreManager.stage) {

			if (currentStage == 0) {
				StartCoroutine	(ChangeColor(TileColor[0],TileColor[2]));
			} else if (currentStage == 1) {
				changeSpeed = 0.0f;
				StartCoroutine	(ChangeColor(TileColor[2],TileColor[1]));
			} else if (currentStage == 2) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor(TileColor[1],TileColor[3]));
			} else if (currentStage == 3) {
				changeSpeed = 0.0f;
				StartCoroutine	(ChangeColor(TileColor[3],TileColor[4]));
			} else if (currentStage == 4) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor(TileColor[4],TileColor[1]));
			}
			else if (currentStage == 5) {
				changeSpeed = 0.0f;
				StartCoroutine	(ChangeColor (TileColor[1],TileColor[5]));
			} else if (currentStage == 6) {
				changeSpeed = 0.0f;
				StartCoroutine (ChangeColor(TileColor[5],TileColor[6]));
			}

			currentStage = ScoreManager.stage;
		}
		*/

	}

	IEnumerator ChangeColor(Material A, Material B)
	{
		float startTime = 0.0f;
		float endTime = 5.0f;
		while (startTime < endTime) {
			startTime += Time.deltaTime;
			changeSpeed += Time.deltaTime * 0.3f;
			meshRender.material.Lerp (A, B, changeSpeed);
			yield return null;
		}

	}

	IEnumerator LastColor(Material A, Material B)
	{
		float startTime = 0.0f;
		float endTime = 5.0f;
		while (startTime < endTime) {
			startTime += Time.deltaTime;
			changeSpeed += Time.deltaTime * 0.3f;
			meshRender.material.Lerp (A, B, changeSpeed);
			Cycle++;
			yield return null;
		}

	}


}
