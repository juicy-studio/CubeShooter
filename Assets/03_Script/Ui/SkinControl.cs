using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinControl : MonoBehaviour {

	public static int redCubeAmount = 0;
	public Text redCubeAmountText;
	public string[] btnAble;

	void Start () {
		if (PlayerPrefs.HasKey ("redcube")) {
			redCubeAmount = PlayerPrefs.GetInt ("redcube");
			redCubeAmountText.text = "" + redCubeAmount;
		} else {
			PlayerPrefs.SetInt ("redcube", redCubeAmount);
		}
	}

	void FixedUpdate () {
		if (redCubeAmount != PlayerPrefs.GetInt ("redcube")) {
			PlayerPrefs.SetInt ("redcube", redCubeAmount);
			redCubeAmountText.text = "" + redCubeAmount;
		}
	}

	public void skinSelect(btnCon btncon)
	{
		btnAble = PlayerPrefs.GetString("btnPrefs").Split(',');
		if (btnAble [btncon.num] == "1") {
			PlayerSkin.skinNum = btncon.num * 2;
			PlayerPrefs.SetInt ("skinNum", PlayerSkin.skinNum);
		}
	}

}
