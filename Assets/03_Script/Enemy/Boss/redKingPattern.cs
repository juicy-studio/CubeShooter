using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redKingPattern : MonoBehaviour {

	public GameObject sucideMinion;
	public GameObject bomb;
	public GameObject bullet;

	public GameObject firePos;
	public GameObject firePos2;

	public GameObject chargeAlert;
	public GameObject chargeEffect;
	public float chargeDamage = 100.0f;
	GameObject player;
	float timer = 4.5f;

	Vector3 randomfirePos;
	float bulletPatternTimer = 5.0f;
	int bulletFlag = 0;

	float minionPatternTimer = 8.0f;
	int minionFlag = 0;

	float bombPatternTimer = 12.0f;
	int bombFlag = 0;

	float chargePatternTimer = 13.5f;
	int chargeFlag = 0;
	float chargeSpeed = 2.0f;
	Vector3 chargePos;
	float textureOffset = 0f;
	MeshRenderer alertImage;
	LineRenderer alertLine;
	ParticleSystem chargeParticleSystem;

	Vector3 relativePos;
	Vector3 lookPos;
	Vector3 rayPos;

	int rotationFlag = 0;
	static List<GameObject> minions;
	static int firstminionCallFlag = 0;
	GameObject minionPool;

	static List<GameObject> bossBullets;
	static int firstBossBulletCallFlag = 0;
	GameObject bossBulletPool;

	// Use this for initialization
	void Awake(){
		alertImage = chargeAlert.GetComponent<MeshRenderer> ();
		alertLine = chargeAlert.GetComponent<LineRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		chargeParticleSystem = chargeEffect.GetComponent<ParticleSystem> ();
		minionPool = GameObject.FindGameObjectWithTag ("MinionPool");
		bossBulletPool = GameObject.FindGameObjectWithTag ("BossBulletPool");
	}

	void Start(){
		if (firstminionCallFlag == 0) {
			minions = new List<GameObject>();
			for (int i = 0; i < 30; i++) {
				GameObject cloneMinion = Instantiate (sucideMinion);
				cloneMinion.transform.parent = minionPool.transform;
				cloneMinion.SetActive (false);
				cloneMinion.GetComponent<BasicEnemyMovement> ().isMinion = true;
				minions.Add (cloneMinion);
			}
			firstminionCallFlag = 1;
		}

		if (firstBossBulletCallFlag == 0) {
			bossBullets = new List<GameObject> ();
			for (int i = 0; i < 80; i++) {
				GameObject bossbullet = Instantiate (bullet);
				bossbullet.transform.parent = bossBulletPool.transform;
				bossbullet.SetActive (false);
				bossBullets.Add (bossbullet);
			}
			firstBossBulletCallFlag = 1;
		}
	}

	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;

		if (rotationFlag == 0) {
			relativePos = player.transform.position - transform.position;
			lookPos = new Vector3 (relativePos.x, 0, relativePos.z);
			transform.rotation = Quaternion.LookRotation (lookPos);
		}

		textureOffset -= Time.deltaTime * 0.4f;
		if (textureOffset < -10f) {
			textureOffset += 10f;
		}
		alertImage.sharedMaterial.SetTextureOffset ("_MainTex", new Vector2 (textureOffset, 0f));

		if (timer >= bulletPatternTimer && bulletFlag == 0) {
			bulletFlag = 1;
			bulletPattern ();
		}

		if (timer >= minionPatternTimer && minionFlag == 0) {
			minionFlag = 1;
			minionPattern ();
		}

		if (timer >= bombPatternTimer && bombFlag == 0) {
			bombFlag = 1;
			callChargeAlert ();
			bombingPattern ();
		}

		if (timer >= chargePatternTimer && chargeFlag == 0) {
			chargeFlag = 1;
			timer = 0;
			//GetComponent<Rigidbody> ().AddForce (transform.forward * 160.0f, ForceMode.Impulse);
		}

		if (chargeFlag == 1) {
			transform.position = Vector3.Lerp(transform.position, chargePos, chargeSpeed * Time.deltaTime);

			chargeParticleSystem.Stop();
			chargeAlert.SetActive (false);
			Invoke ("flagClear", 0.7f);
		}

	}

	void flagClear(){
		bulletFlag = 0;
		minionFlag = 0;
		bombFlag = 0;
		chargeFlag = 0;
		rotationFlag = 0;
		chargeEffect.SetActive (false);
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Player") {
			player.GetComponent<PlayerHealth> ().TakeDamage (chargeDamage);
		}
	}

	void bulletPattern (){
		StartCoroutine ("bulletFire");

	}

	void minionPattern(){
		for (int i = 0; i < 30; i++) {
			if (!minions [i].activeInHierarchy) {
				Vector3 randomPos = new Vector3 (
					Random.Range (transform.position.x-12.0f, transform.position.x+12.0f),
					0.4f, Random.Range(transform.position.z-12.0f, transform.position.z+12.0f));
				minions [i].transform.position = randomPos;
				minions [i].transform.rotation = Quaternion.identity;
				minions [i].SetActive (true);
			}
		}
	}

	void callChargeAlert (){
		chargePos = player.transform.position;
		chargePos.y = 2.5f;
		rotationFlag = 1;
		alertLine.SetPosition (0, new Vector3(transform.position.x,0.2f,transform.position.z));
		alertLine.SetPosition (1, new Vector3(player.transform.position.x,0.2f,player.transform.position.z));
		chargeEffect.SetActive (true);
		chargeParticleSystem.Play ();
		chargeAlert.SetActive (true);
	}

	void bombingPattern (){
		StartCoroutine ("sommnonBomb");
	}

	void chargePattern(){
		StartCoroutine ("charging");
	}
		
	IEnumerator bulletFire(){
		for(int i = 0; i<40;i++){
			if (!bossBullets [i].activeInHierarchy) {
				randomfirePos = firePos.transform.position;
				randomfirePos.x = Random.Range (firePos.transform.position.x - 3.0f, firePos.transform.position.x + 3.0f);
				bossBullets [i].transform.position = randomfirePos;
				bossBullets [i].transform.rotation = firePos.transform.rotation;
				bossBullets [i].SetActive (true);

				randomfirePos = firePos2.transform.position;
				randomfirePos.x = Random.Range (firePos2.transform.position.x - 3.0f, firePos2.transform.position.x + 3.0f);
				bossBullets [i+40].transform.position = randomfirePos;
				bossBullets [i+40].transform.rotation = firePos.transform.rotation;
				bossBullets [i+40].SetActive (true);
			}

			yield return new WaitForSeconds (0.2f);
		}
	}

	IEnumerator sommnonBomb(){
		for (int i = 0; i <15; i++) {
			GameObject clonbBomb = Instantiate (bomb, new Vector3 (Random.Range (player.transform.position.x - 10f, player.transform.position.x + 10f),
				0.1f, Random.Range (player.transform.position.z - 10f, player.transform.position.z + 10f))
				, this.transform.rotation);
			clonbBomb.GetComponentInChildren<MeshRenderer> ().material = gameObject.GetComponent<MeshRenderer> ().material;
			yield return new WaitForSeconds (0.4f);
		}
	}

}
