using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRange : MonoBehaviour {

    public float damage = 50;
    ParticleSystem particle;
    CapsuleCollider collider;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        collider = GetComponent<CapsuleCollider>();
        particle.Stop();
        particle.Play();
        Invoke("TriggerOff", 0.5f);
        Destroy(gameObject, 2f);
    }

    void TriggerOff()
    {
        collider.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            BasicEnemyMovement enemyHealth = col.GetComponent<BasicEnemyMovement>();
            if (enemyHealth != null)
            {
                enemyHealth.takeDamaged(damage, Vector3.forward);
            }
        }
    }
}
