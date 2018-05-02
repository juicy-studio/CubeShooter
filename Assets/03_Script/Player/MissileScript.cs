using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{    
    public GameObject range;
    public float speed = 0.1f;
    public float accel = 1f;

    void OnEnable()
    {        
        Invoke("Destroy", 2f);
        speed = 0.1f;
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
        speed += accel;
        float moveDistance = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * moveDistance);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Map")
        {
            gameObject.SetActive(false);
            range.transform.position = gameObject.transform.position;
            Instantiate(range);
        }
    }
}