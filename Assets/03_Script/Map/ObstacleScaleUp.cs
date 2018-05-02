using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScaleUp : MonoBehaviour {

	public float obstacleHeight;
	public Vector3 startScale;
	public Vector3 endScale;
	bool IsFinished;
	private float moveUp;
	private float moveDown;
	int currentStage;
	private float RandomSpeed;
	Vector3 Scale;
	Transform Obstacles;

	// Use this for initialization
	void Start()
	{
		Obstacles = GetComponent<Transform> ();
		startScale = Obstacles.localScale;
		endScale = Obstacles.localScale;
		endScale.y = Obstacles.localScale.x;
		RandomSpeed = Random.Range (0.3f, 0.8f);
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
			moveUp += Time.deltaTime * RandomSpeed;

			transform.localScale = Vector3.Lerp(startScale, endScale, moveUp);


		}else
		{

			moveDown += Time.deltaTime * 0.6f;

			transform.localScale = Vector3.Lerp (endScale, startScale, moveDown);
		}

	}

	IEnumerator StageChanged()
	{
		IsFinished = true;
		yield return new WaitForSeconds (2.0f);
		IsFinished = false;

	}
}
