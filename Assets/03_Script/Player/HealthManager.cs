using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public static float starthealth;
    public static float currentHealth;
    Text text;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        starthealth = 100;
        currentHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = (int)currentHealth + " / " + (int)starthealth;
	}
}
