using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUP : MonoBehaviour {

	public float speed = 0.5f;
	Vector3 startPos;
	Vector3 endPos;
	public bool IsFinished = false;
	private float moveUp;
	private float moveDown;
	int currentStage;


	// Use this for initialization
	void Start()
	{
		startPos = transform.position;
		endPos = transform.position+Vector3.up * 4.5f;
		currentStage = ScoreManager.stage;
	}

	// Update is called once per frame
	void Update()
	{
		if (currentStage < ScoreManager.stage) {
			StartCoroutine (StageChanged ());
			currentStage = ScoreManager.stage;
		}

		if (IsFinished == false)
		{
			moveUp += Time.deltaTime * 0.5f;

			transform.position = Vector3.Lerp(startPos, endPos, moveUp);


		}else
		{

			moveDown += Time.deltaTime * 0.5f;

			transform.position = Vector3.Lerp (endPos, startPos, moveDown);
		}


	}
	IEnumerator StageChanged()
	{
		IsFinished = true;
		yield return new WaitForSeconds (2.0f);
		IsFinished = false;

	}




}
