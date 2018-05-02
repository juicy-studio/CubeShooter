using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeUP : MonoBehaviour
{
    public float obstacleHeight;
    public float speed = 0.5f;
    public Vector3 startPos;
    public Vector3 endPos;
    bool IsFinished;
	private float moveUp;
	private float moveDown;
	int currentStage;
	private float RandomSpeed;

    // Use this for initialization
    void Start()
    {
		RandomSpeed = Random.Range (0.1f, 1f);
		obstacleHeight = Random.Range (3, 6);
        startPos = transform.position;
        endPos = transform.position + Vector3.up * obstacleHeight;
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