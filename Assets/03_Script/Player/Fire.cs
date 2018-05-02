using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public float bulletDamage = 10;

    ParticleSystem particle;

    void OnEnable()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        Invoke("Destroy", 1f);
    }

    void Destroy()
    {
        particle.Stop();
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
