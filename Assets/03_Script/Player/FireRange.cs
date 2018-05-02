using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRange : MonoBehaviour {

    public float damage = 0.01f;
    void OnTriggerStay(Collider col)
    {
            if (col.tag == "Enemy")
            {
                BasicEnemyMovement enemyHealth = col.GetComponent<BasicEnemyMovement>();
                if (enemyHealth != null)
                {
                    enemyHealth.takeDamaged(damage, col.transform.position);
                }
            }
    }
}
