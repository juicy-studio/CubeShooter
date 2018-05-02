using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 11.0f;
	public GameObject firePos;
	public GameObject enemyLaser;
	public LayerMask collisionMask;
	public float attackRange = 18.0f;
	public BoxCollider damagedRange;
	public float laserdamage = 100.0f;

	UnityEngine.AI.NavMeshAgent agent;
	Animator anim;
	GameObject player;
	GameObject cloneLaser;
	public GameObject bulletHolder;
	float timer = 11.0f;

	PlayerHealth playerhealth;
	BasicEnemyMovement basicenemy;

	Vector3 relativePos;
	Vector3 lookPos;
	Vector3 rayPos;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
		playerhealth = player.GetComponent<PlayerHealth> ();
		basicenemy = GetComponent<BasicEnemyMovement> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		bulletHolder = GameObject.FindGameObjectWithTag("BulletPool");

		cloneLaser = Instantiate (enemyLaser);
		cloneLaser.transform.parent = bulletHolder.transform;
		cloneLaser.SetActive (false);
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (basicenemy.isEnemyDead == true) {
			isdead ();
		}

		if (agent.isStopped == true) {
			checkCollision ();
		}

		if (basicenemy.isEnemyDead) {
			basicenemy.isEnemyDead = false;
		}

		rayPos = transform.position;
		rayPos.y = 0.3f;
	
		if (timer >= timeBetweenAttacks) {
			agent.isStopped = false;
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
		agent.isStopped = true;
		anim.SetTrigger ("Attack");
		cloneLaser.transform.position = firePos.transform.position;
		cloneLaser.transform.rotation = firePos.transform.rotation;
		cloneLaser.SetActive (true);
		cloneLaser.GetComponent<Animator> ().enabled = true;
		cloneLaser.GetComponent<Animator> ().SetTrigger ("startLaser");
		cloneLaser.GetComponentInChildren<ParticleSystem> ().Play ();
	}

	void checkCollision(){
		ParticleSystem laserPasrticleComp = cloneLaser.GetComponentInChildren<ParticleSystem> ();
		if (laserPasrticleComp.time >= 0.1f) {
			cloneLaser.GetComponent<Animator> ().enabled = false;
			damagedRange.gameObject.transform.rotation = cloneLaser.transform.rotation;
			damagedRange.gameObject.SetActive (true);
		}
		if (laserPasrticleComp.time >= 5.0f) {
			damagedRange.gameObject.SetActive (false);
		}
	}

	void OnTriggerStay(Collider coll){
		if (coll.tag == "Player") {
//			print ("checked");
			playerhealth.TakeDamage (laserdamage);
		}
	}


	void isdead(){
		cloneLaser.GetComponentInChildren<ParticleSystem> ().Stop ();
		cloneLaser.GetComponent<Animator> ().enabled = false;
		cloneLaser.SetActive (false);
		damagedRange.gameObject.SetActive (false);
		agent.isStopped = false;
		basicenemy.isEnemyDead = false;
		timer = 11.0f;
	}
}
