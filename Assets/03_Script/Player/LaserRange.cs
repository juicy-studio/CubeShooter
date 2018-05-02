using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRange : MonoBehaviour {

    public float damage = 2;

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
