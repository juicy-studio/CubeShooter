using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnCon : MonoBehaviour {

    public int num;
    public Sprite texture;
    Image image;
    public string[] btnAble;
    
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        btnAble = PlayerPrefs.GetString("btnPrefs").Split(',');

        if (btnAble[num] == "1")
            image.sprite = texture;
    }
	
	// Update is called once per frame
	void Update () {
        
            btnAble = PlayerPrefs.GetString("btnPrefs").Split(',');
        
        if (btnAble[num]=="1")
            image.sprite = texture;
    }
}
