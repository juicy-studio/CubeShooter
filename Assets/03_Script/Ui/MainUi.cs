using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUi : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionMenuHolder;
	bool isOptionMenu;
	public static bool isMusicOn;
	public GameObject MusicOnBtt;
	public GameObject MusicOffBtt;
	public GameObject CreditHolder;
	public bool isCredit;

	//skinshop====================================
	public GameObject skinShopHolder;

	// Use this for initialization
	void Awake () {
        mainMenuHolder.SetActive (true);
		optionMenuHolder.SetActive (false);
		CreditHolder.SetActive (false);
		skinShopHolder.SetActive (false);
		isMusicOn = true;
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (isCredit) {
				CreditToOption ();
			}
			else if (isOptionMenu) {
				MainMenu ();
			} else {
				Application.Quit ();
			}
		}

	}

	public void StartGame()
	{

		Application.LoadLevel ("LoadingScreen");
	}

	public void Quitgame()
	{

		Application.Quit();
	}

	public void OptionMenu(){
		mainMenuHolder.SetActive (false);
		optionMenuHolder.SetActive (true);
		isOptionMenu = true;
	}


	public void MainMenu(){
		mainMenuHolder.SetActive (true);
		optionMenuHolder.SetActive (false);
		skinShopHolder.SetActive (false);
		isOptionMenu = false;
		
	}

	public void musicOff()
	{
		isMusicOn = false;
		MusicOnBtt.SetActive (false);
		MusicOffBtt.SetActive (true);

	}

	public void musicOn()
	{
		isMusicOn = true;
		MusicOnBtt.SetActive (true);
		MusicOffBtt.SetActive (false);

	}

	public void Credit()
	{
		CreditHolder.SetActive (true);
		optionMenuHolder.SetActive (false);
		
	}

	public void CreditToOption()
	{
		CreditHolder.SetActive (true);
		optionMenuHolder.SetActive (false);

	}

	public void skinShopMunu(){
		mainMenuHolder.SetActive (false);
		skinShopHolder.SetActive (true);
	}
}
