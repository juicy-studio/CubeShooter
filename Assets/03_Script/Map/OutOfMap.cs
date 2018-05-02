using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMap : MonoBehaviour {


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			GameObject.FindWithTag ("Player").transform.position = new Vector3 (0, 0.5f, 0);
		}

	}
}
