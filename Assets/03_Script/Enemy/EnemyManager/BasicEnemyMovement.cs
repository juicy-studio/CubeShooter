using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
	Transform player;
	UnityEngine.AI.NavMeshAgent nav;

	public float originHP;
	public GameObject deadParticles;
	public int scoreValue;
	public AudioClip hitSound;
	public AudioClip dieSound;

	float enemyHP;
	ParticleSystem hitParticles;
	Animator anim;
    float speed;
    float timer=0f;
	public bool isEnemyDead = false;
	GameObject cloneDeadParticle;
	//ParticleSystem dieParticles;

	public bool isBoss = false;
	public bool isMinion = false;

	AudioSource monsterAudio;
	float currTime = 0;
	float audioBetweenPlay = 0.1f;
	Vector3 randomPos;

	void Awake ()
	{
		hpReset ();
	
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        hitParticles = GetComponentInChildren <ParticleSystem> ();
		monsterAudio = GameObject.FindGameObjectWithTag ("EnemyManager").GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		if (isBoss == false) {
			nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
			speed = nav.speed;
		}
		callRandomPos ();
	}

	void Update ()
	{

		timer += Time.deltaTime;
		if (isBoss == false) {
			if (Vector3.Distance (transform.position, player.transform.position) <= 35.0f) {
				nav.SetDestination (player.position);

				if (nav.speed < speed && timer >= 0.5f) {
					speedRecover ();
				}
			} else if (isMinion) {
				nav.SetDestination (player.position);
			} else if (!isMinion) {
				nav.SetDestination (randomPos);
				Invoke ("callRandomPos", 1.0f);
			}
		}
        
	}

	void callRandomPos(){
		randomPos = new Vector3 (
			Random.Range (-350.0f,350.0f),
			transform.position.y,
			Random.Range(-350.0f,350.0f));
	}

	public void takeDamaged(float damage, Vector3 hitPoint){
		if (currTime == 0) {
			currTime = timer;
			monsterAudio.Stop ();
			monsterAudio.PlayOneShot (hitSound);
		} else {
			if ((timer - currTime) >= audioBetweenPlay) {
				monsterAudio.Stop ();
				monsterAudio.PlayOneShot (hitSound);
			}
			currTime = 0;
		}
		enemyHP -= damage;
		hitParticles.transform.position = hitPoint;
		gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = this.gameObject.GetComponent<MeshRenderer> ().material;
		hitParticles.Play();

		anim.SetTrigger ("Hit");
		if (enemyHP <= 0) {
			if (isMinion == true) {
				noScoreDeath ();
			} else {
				Death ();
			}
		}
 
	}

	public void Death (){
		if (isBoss) {
			SkinControl.redCubeAmount = PlayerPrefs.GetInt ("redcube");
			SkinControl.redCubeAmount++;
			PlayerPrefs.SetInt ("redcube", SkinControl.redCubeAmount);
		}
			
		isEnemyDead = true;
		this.gameObject.SetActive (false);
		ScoreManager.score += scoreValue;
		(cloneDeadParticle = Instantiate (deadParticles,this.transform.position,Quaternion.identity)).GetComponent<ParticleSystemRenderer>().material = this.GetComponent<MeshRenderer> ().material;
		cloneDeadParticle.GetComponent<AudioSource> ().clip = dieSound;
		cloneDeadParticle.GetComponent<AudioSource> ().Play ();
		Destroy (cloneDeadParticle, 2.0f);
	}

	public void noScoreDeath (){
		isEnemyDead = true;
		this.gameObject.SetActive (false);
		(cloneDeadParticle = Instantiate (deadParticles,this.transform.position,Quaternion.identity)).GetComponent<ParticleSystemRenderer>().material = this.GetComponent<MeshRenderer> ().material;
		cloneDeadParticle.GetComponent<AudioSource> ().clip = dieSound;
		cloneDeadParticle.GetComponent<AudioSource> ().Play ();
		Destroy (cloneDeadParticle, 2.0f);
	}

    public void takeMoved(Vector3 targetPosition)
    {
		if (isBoss == false) {
			this.transform.position = Vector3.Lerp (this.transform.position, targetPosition, 5f * Time.deltaTime);
		}
    }

    public void backOff()
    {
		if (isBoss == false) {
			this.transform.position = Vector3.Lerp (this.transform.position, this.transform.position - this.transform.forward, 35f * Time.deltaTime);
		}
    }

    public void speedDown()
    {
        if(nav.speed > 1)
        {
            nav.speed -= 1;
        }
        timer = 0f;
    }

    public void speedRecover()
    {
        nav.speed += 1;
        timer = 0f;
    }

	public void hpReset(){
		enemyHP = originHP;
	}

    public void colorChanges()
    {

    }
}