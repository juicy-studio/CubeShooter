using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBullet : MonoBehaviour {

    public float damage = 100f;
    public float speed = 35f;

    void OnEnable()
    {
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
        transform.Translate(Vector3.forward * moveDistance);
    }

    void OnTriggerEnter(Collider col)
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
