using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerBombCtrl : MonoBehaviour {

	public float delayedTime = 2.0f;
	public float bombDamage = 50;
	public GameObject explosion;
	public GameObject redMark;
	public GameObject bomb;

	float timer;
	SphereCollider damageArea;
	PlayerHealth playerhealth;

	void OnEnable()
	{
		Destroy (gameObject, 2.5f);
	}
	void Awake(){
		playerhealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		damageArea = GetComponent<SphereCollider> ();
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.CompareTag ("Player")) {
			playerhealth.TakeDamage (bombDamage);
		}
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= delayedTime) {
			timer = 0;
			bomb.SetActive (false);
			redMark.SetActive (false);
			damageArea.enabled = true;
			Instantiate (explosion, gameObject.transform);
		}
	}
}
