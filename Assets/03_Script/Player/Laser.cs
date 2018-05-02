using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    ParticleSystem particleSystem;
    Light light;
    bool particleMotion;
    bool animMotion;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
        particleSystem.Stop();
        light.enabled = false;
    }

    void Shoot()
    {

    }
}
