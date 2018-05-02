using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleRangedAttack : MonoBehaviour {

	public float timeBetweenAttacks = 1.5f;
	public LayerMask collisionMask;
	public float attackRange = 15.0f;

	public GameObject bullet;
	public GameObject bulletHolder;
	public List<GameObject> bulletPool;

	Animator anim;
	GameObject player;
	float timer;
	int countEight = 0;
	BasicEnemyMovement basicEnemy;

	Vector3 relativePos;
	Vector3 lookPos;
	Vector3 rayPos;

	void Awake ()
	{
		basicEnemy = GetComponent<BasicEnemyMovement> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		bulletHolder = GameObject.FindGameObjectWithTag("BulletPool");
		anim = GetComponent <Animator> ();
		bulletPool = new List<GameObject> ();

		for (int i = 0; i < 40; i++) {
			GameObject circleBullet = Instantiate (bullet);
			circleBullet.transform.parent = bulletHolder.transform;
			circleBullet.SetActive (false);
			bulletPool.Add (circleBullet);
		}
			
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
		countEight = 0;
		anim.SetTrigger ("Attack");
		for (int i = 0; i < bulletPool.Count; i++) {
			if (!bulletPool [i].activeInHierarchy && !(countEight == 8)) {
				bulletPool [i].transform.position = gameObject.transform.position;
				Quaternion angle = Quaternion.identity;
				angle.eulerAngles = new Vector3 (0, 45.0f * countEight, 0);
				bulletPool [i].transform.rotation = angle;
				bulletPool [i].SetActive (true);
				countEight++;
			}
		}
	}
}
