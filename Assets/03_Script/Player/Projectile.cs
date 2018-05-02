using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionMask;
    public float bulletDamage = 10;
    public bool ShotGun = false;
    public bool IceGun = false;
    public bool RandomGun = false;
    float speed = 35f;
    Renderer color;
    
    void OnEnable()
    {
        color = GetComponent<MeshRenderer>();

        if (RandomGun)
        {
            color.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        if (ShotGun)
        {
            speed = Random.Range(35f, 44f);
        }
        
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        float moveDistance = Time.deltaTime * speed;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }
    
    void CheckCollisions (float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject (RaycastHit hit)
    {
//        print(hit.collider.gameObject.name);
        gameObject.SetActive(false);
		if (hit.collider.tag == "Enemy") {
			BasicEnemyMovement enemyHealth = hit.collider.GetComponent <BasicEnemyMovement> ();
			if (enemyHealth != null) {
				enemyHealth.takeDamaged (bulletDamage, hit.point);
                if(ShotGun)
                {
                    enemyHealth.backOff();
                }
                if(IceGun)
                {
                    enemyHealth.speedDown();
                }
			}
		} else if (hit.collider.tag == "Map") {
    
		}
	}
    
}