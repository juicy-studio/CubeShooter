using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour {
	public float delayTime = 1.0f;
	Animator anim;
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();

		Invoke ("Pop", delayTime);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Pop()
	{
		anim.SetBool ("IsPop", true);
	}
}
