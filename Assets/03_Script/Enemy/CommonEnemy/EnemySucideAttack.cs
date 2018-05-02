using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySucideAttack : MonoBehaviour {
	
	public int attackDamage = 50;

	GameObject player;
	bool playerInRange;

	PlayerHealth playerhealth;
	BasicEnemyMovement basicEnemy;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
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

	// Update is called once per frame
	void Update () {

		if (basicEnemy.isEnemyDead) {
			basicEnemy.isEnemyDead = false;
		}

		if (playerInRange) {
			playerInRange = false;
			playerhealth.TakeDamage(attackDamage);
			basicEnemy.noScoreDeath ();
		}
	}
}
