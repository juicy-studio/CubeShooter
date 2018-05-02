using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour {

	Text text;

	void Start () {
		text = GetComponent<Text>();

	}

	// Update is called once per frame
	void Update () {

		PointRecord.Points = ScoreManager.score/10 - PointRecord.SpendPoint;
		text.text = PointRecord.Points+"";

	}
}
