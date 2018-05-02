using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuicyStudioScene : MonoBehaviour {

	public float delayTime =3.0f;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (delayTime);

		Application.LoadLevel ("Main");

	}

	// Update is called once per frame
	void Update () {

	}
}
