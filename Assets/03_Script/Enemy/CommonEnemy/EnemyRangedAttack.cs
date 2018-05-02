using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{

	public float timeBetweenAttacks = 0.5f;
	public Transform firePos;
	public LayerMask collisionMask;
	public float attackRange = 18.0f;

	public GameObject bullet;
	public GameObject bulletHolder;
	public List<GameObject> bulletPool;

	Animator anim;
	GameObject player;
	float timer;

	BasicEnemyMovement basicEnemy;

	void Awake ()
	{
		basicEnemy = GetComponent<BasicEnemyMovement> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		bulletHolder = GameObject.FindGameObjectWithTag("BulletPool");
		anim = GetComponent <Animator> ();
		bulletPool = new List<GameObject> ();
		for (int i = 0; i < 8; i++) {
			GameObject circleBullet = Instantiate (bullet);
			circleBullet.transform.parent = bulletHolder.transform;
			circleBullet.SetActive (false);
			bulletPool.Add (circleBullet);
		}
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (basicEnemy.isEnemyDead) {
			basicEnemy.isEnemyDead = false;
		}

		Vector3 relativePos = player.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation (relativePos);

		if (timer >= timeBetweenAttacks) {
			Ray ray = new Ray (transform.position, transform.forward);
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
		//Instantiate (bullet, firePos.position,this.transform.rotation);

		for (int i = 0; i < bulletPool.Count; i++) {
			if (!bulletPool [i].activeInHierarchy) {
				bulletPool [i].transform.position = firePos.transform.position;
				bulletPool [i].transform.rotation = transform.rotation;
				bulletPool [i].SetActive (true);
				break;
			}
		}
			
	}
}
