using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLaserCtrl : MonoBehaviour {

	MeshRenderer meshRend;

	private float textureOffset = 0f;

	// Use this for initialization
	void Start () {
		meshRend = GetComponentInChildren<MeshRenderer> ();
	}

	void Awake(){
	}
	
	// Update is called once per frame
	void Update () {
		
		textureOffset -= Time.deltaTime * 0.2f;
		if (textureOffset < -10f) {
			textureOffset += 10f;
		}

		meshRend.sharedMaterials [0].SetTextureOffset ("_MainTex", new Vector2 (textureOffset, 0f));
	}
}
