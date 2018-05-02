using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonEnemyBulletMove : MonoBehaviour {

	public LayerMask collisionMask;
    public float bulletDamage = 10f;
	public float bulletSpeed = 10.0f;
//	float lifetime = 10.0f;

    GameObject player;
    PlayerHealth playerHealth;

	Vector3 rayPos;

    void OnEnable()
	{
		//Invoke ("Destroy", 4f);
	}

	void Destroy(){
		gameObject.SetActive (false);
	}

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update ()
	{
		float moveDistance = Time.deltaTime * bulletSpeed;
		CheckCollisions(moveDistance);
		transform.Translate(Vector3.forward * moveDistance);
		//transform.Translate (Vector3.forward * bulletSpeed * Time.deltaTime);
	}
	/*
    void OnTriggerEnter(Collider col)
    {
		print (col.gameObject.name);
        if(col.CompareTag("Player"))
        {

			gameObject.SetActive(false);
            playerHealth.TakeDamage(bulletDamage);
		}else if(col.CompareTag("Map")){

			gameObject.SetActive(false);
		}
    }
*/
	void CheckCollisions (float moveDistance)
	{
		rayPos = transform.position;
		//rayPos.y = 
		Ray ray = new Ray(rayPos, transform.forward);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
		{
			OnHitObject(hit);
		}
	}

	void OnHitObject (RaycastHit hit)
	{
		gameObject.SetActive(false);
		if (hit.collider.tag == "Player") {
			playerHealth.TakeDamage(bulletDamage);
		}else if(hit.collider.CompareTag("Map")){
		}
	}
}
