using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomerAttack : MonoBehaviour
{

	public float timeBetweenAttacks = 1.0f;
	public Transform firePos;
	public GameObject fireEffect;
	public GameObject bomb;
	public LayerMask collisionMask;
	public float attackRange = 18.0f;

	Animator anim;
	GameObject player;
	float timer;

	Vector3 relativePos;
	Vector3 lookPos;
	Vector3 rayPos;

	BasicEnemyMovement basicEnemy;
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
		basicEnemy = GetComponent<BasicEnemyMovement> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

		relativePos = player.transform.position - transform.position;
		lookPos = new Vector3 (relativePos.x, 0, relativePos.z);
		transform.rotation = Quaternion.LookRotation (lookPos);


		if (basicEnemy.isEnemyDead) {
			basicEnemy.isEnemyDead = false;
		}

		rayPos = transform.position;
		rayPos.y = 0.5f;
		if (timer >= timeBetweenAttacks) {
			Ray ray = new Ray (rayPos, transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, attackRange, collisionMask, QueryTriggerInteraction.Collide)) {
				if (hit.collider.tag == "Player")
					Attack (hit);
			}
		}
	}

	void Attack (RaycastHit hit)
	{
		timer = 0f;
		anim.SetTrigger ("Attack");
		Instantiate (fireEffect, firePos.position,this.transform.rotation);
		Instantiate (bomb, new Vector3(Random.Range(hit.point.x-1.5f,hit.point.x+1.5f),
			0.1f,Random.Range(hit.point.z-1.5f,hit.point.z+1.5f))
			,this.transform.rotation);
	}
}
