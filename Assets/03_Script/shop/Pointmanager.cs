using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pointmanager : MonoBehaviour {

	Text text;

	void Start () {
		text = GetComponent<Text>();
	//	gunController = GetComponent<GunController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		PointRecord.Points = ScoreManager.score/10 - PointRecord.SpendPoint;
		text.text = PointRecord.Points + "$";

	}

}
