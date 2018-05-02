using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUi : MonoBehaviour {

	Animator anim;
	public GameObject Buttons;
	public GameObject Img;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Img.SetActive (false);
		Buttons.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.gameOver) {
			Img.SetActive (true);
			Buttons.SetActive (true);
			anim.SetBool ("GameEnd", true);
		} else {
			anim.SetBool ("GameEnd", false);
		}
		
	}
}

