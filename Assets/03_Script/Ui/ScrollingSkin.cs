using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingSkin : MonoBehaviour {
	public RectTransform panel; 
	public Button[] bttn;
	public RectTransform center; //center to compare the distance for each buttion    
	public float[] distance; //each button's center
	public float[] distReposition;
	public GameObject randomChoiceLight;

	private bool dragging = false;
	private int bttnDistance;
	private int minButtonNum; //hold the number of the button, wuth smallest distance to center
	private int bttnLength;

	public string btnPrefs = "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string[] btnAble;
    static public int sendbtnlength = 41;

    void Awake()
    {
		
        if (PlayerPrefs.HasKey("btnPrefs"))
        {
            btnAble = PlayerPrefs.GetString("btnPrefs").Split(',');
        }
        else
        {
            PlayerPrefs.SetString("btnPrefs", btnPrefs);
        }

    }

    void Start()
	{        
		PlayerPrefs.SetString("btnPrefs", "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
		bttnLength = bttn.Length;
		distance = new float[bttnLength];
		distReposition = new float[bttnLength];

		bttnDistance = (int)Mathf.Abs (bttn [1].GetComponent<RectTransform> ().anchoredPosition.x - bttn [0].GetComponent<RectTransform> ().anchoredPosition.x);

        btnAble = PlayerPrefs.GetString("btnPrefs").Split(',');
    }
		
	void Update () {
		for (int i = 0; i < bttn.Length; i++) {
			distReposition [i] = center.GetComponent<RectTransform> ().position.x - bttn [i].GetComponent<RectTransform> ().position.x;
			distance [i] = Mathf.Abs (distReposition [i]);

			if (distReposition [i] > 8000) {
				float curX = bttn [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY = bttn [i].GetComponent<RectTransform> ().anchoredPosition.y;

				Vector2 newAnchoredPos = new Vector2 (curX + (bttnLength * bttnDistance), curY);
				bttn [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos;
			}

			if (distReposition [i] < -8000) {

				float curX = bttn [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY = bttn [i].GetComponent<RectTransform> ().anchoredPosition.y;

				Vector2 newAnchoredPos = new Vector2 (curX - (bttnLength * bttnDistance), curY);
				bttn [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos;
			}
		}

		float minDistance = Mathf.Min (distance);

		for (int i = 0; i < bttn.Length; i++) {
			if (minDistance == distance [i]) {
				minButtonNum = i;
			}
		}

		if (!dragging) {
			//LerpToBttn (minButtonNum * -bttnDistance);
			LerpToBttn(-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x+20);
		}
	}
		
	void LerpToBttn(float position){
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPosition;
	}

	public void startDrag()
	{
		dragging = true;
	}

	public void endDrag()
	{
		dragging = false;
	}

	public void randomSelect(){
		if (SkinControl.redCubeAmount != 0) {
			SkinControl.redCubeAmount--;
			int randomBoxNum = UnityEngine.Random.Range (1, bttn.Length - 1);
			if (btnAble [randomBoxNum] == "0") {
				String tmpPrefs = "";
				btnAble [randomBoxNum] = "1";

				for (int i = 0; i < 41; i++) {
					tmpPrefs += btnAble [i];
					if (i < 40)
						tmpPrefs += ",";
				}
				PlayerPrefs.SetString ("btnPrefs", tmpPrefs);
			}
			print (randomBoxNum);
			float newX = Mathf.Lerp (panel.anchoredPosition.x, -bttn [randomBoxNum].GetComponent<RectTransform> ().anchoredPosition.x, Time.deltaTime * 220f);
			Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);
			panel.anchoredPosition = newPosition;
		}

	}

	public void pauseSkinBuy(Button buyButton){
		if (SkinControl.redCubeAmount != 0) {
			StartCoroutine ("RandomSkinSelect", buyButton);
		}
	}

	IEnumerator RandomSkinSelect(Button buyButton){
		buyButton.interactable = false;
		randomChoiceLight.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		randomChoiceLight.SetActive (false);
		buyButton.interactable = true;
	}
}
