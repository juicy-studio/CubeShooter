using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : MonoBehaviour {

    public float damage = 1f;
    public float speed = 5f;
    void OnEnable()
    {
        Invoke("Destroy", 4f);
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
        transform.Translate(Vector3.forward * moveDistance);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Enemy")
        {
            BasicEnemyMovement enemyHealth = col.GetComponent<BasicEnemyMovement>();
            if (enemyHealth != null)
            {
                enemyHealth.takeDamaged(damage, col.transform.position);
                enemyHealth.takeMoved(this.transform.position);
            }
        }
    }
}
