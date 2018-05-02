using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.6f;
	public int attackDamage = 10;
	Animator anim;
	GameObject player;
	bool playerInRange;
	float timer;

	PlayerHealth playerhealth;
	BasicEnemyMovement basicEnemy;
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
		playerhealth = player.GetComponent<PlayerHealth> ();
		basicEnemy = GetComponent<BasicEnemyMovement> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}


	void Update ()
	{
		
		timer += Time.deltaTime;
		if (basicEnemy.isEnemyDead) {
			playerInRange = false;
			basicEnemy.isEnemyDead = false;
		}
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			Attack ();
		}
	}


	void Attack ()
	{
		timer = 0f;
		anim.SetTrigger ("Attack");
		playerhealth.TakeDamage(attackDamage);
	}
}
