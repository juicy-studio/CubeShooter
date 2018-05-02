using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour {

	public static bool gameOver;
	public GameObject Buttons;
	public static bool isPause;
	public GameObject PauseUi;
	public GameObject player;
	public GameObject gameoverUIs;
	public GameObject gameoverImg;
	public GameObject JSAim;
	public GameObject Js;
	public Button ReviveBtt;
	bool isRevived;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindWithTag ("Player");
		gameOver = false;
		Buttons.SetActive (false);
		isPause = false;
		PauseUi.SetActive (false);
		Application.targetFrameRate = 40;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				BackToMenu ();
			}
			
		} else {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (isPause) {
					Resume ();
				} else if (isPause == false) {
					Pause ();
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.I)) {
		
			ScoreManager.score += 500;
		}

	}

	public void BackToMenu()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("Main");

	}

	public void Revive()
	{
		if (isRevived==false) {
			player.GetComponent<PlayerHealth> ().Heal ();
			player.SetActive (true);
			player.GetComponent<PlayerHealth> ().TakeDamage (0f);
			PlayerHealth.isdead = false;
			isRevived = true;

			//boss stage bomb clear
			GameObject[] notcleardbomb = GameObject.FindGameObjectsWithTag("boomerbomb");
			foreach (GameObject obj in notcleardbomb) {
				Destroy (obj, 0f);
			}

			gameoverUIs.SetActive (false);
			gameoverImg.SetActive (false);
			gameOver = false;
			Resume ();
			ReviveBtt.interactable = false;
		}


	}

	public void Pause()
	{
		if (isPause == false) {
			Time.timeScale = 0;
			PauseUi.SetActive (true);
			isPause = true;
			JSAim.SetActive (false);
			Js.SetActive (false);
		}
	}

	public void Resume()
	{
		if (isPause) {
			PauseUi.SetActive (false);
			Time.timeScale = 1;
			isPause = false;
			CountDown.countTimer = 0;
			JSAim.SetActive (true);
			Js.SetActive (true);
		
		}
	}
		

}
