using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public enum FireMode { BasicGun, MachineGun, ShotGun, FireGun, MissileGun, LaserGun, RandomGun, Machine2 };
    public FireMode fireMode;

    public Transform[] bulletSpawn;
    public GameObject projectile;
    public float msBetweenShots = 0.1f;
    public int pooledAmount = 500;
    AudioSource gunAudio;
    AudioSource laserAudio;
    public bool isRare;

    float timer = 10.0f;
    float Lasertimer = 7f;
    bool isfire = false;
    Collider Range;
    GameObject FireRange;
    ParticleSystem gunParticle;
    ParticleSystem particle;
    ParticleSystem laserparticle;

    //추가
    public string GunName;
    public int GunPrice;
    public Texture GunImage;
    private VirtualJSAim jsAim;

    bool triggerRealeasedSinceLastShot;

    List<GameObject> bullets;

    void Start()
    {
        if (fireMode == FireMode.FireGun)
        {
            FireRange = GameObject.Find("FireRange");
            particle = GameObject.Find("ParticleSystem").GetComponent<ParticleSystem>();
            isfire = false;

            jsAim = GameObject.Find("Player").GetComponent<PlayerShooting>().JsAim;
        }
        if(fireMode == FireMode.LaserGun)
        {            
            Range = GameObject.Find("LaserRange").GetComponent<BoxCollider>();
            particle = GameObject.Find("Laser").GetComponent<ParticleSystem>();
            laserparticle = GameObject.Find("LaserParticle").GetComponent<ParticleSystem>();
            laserAudio = GetComponent<AudioSource>();
            particle.Stop();
            laserparticle.Stop();
        }        
        gunParticle = GetComponentInChildren<ParticleSystem>();
        bullets = new List<GameObject>();
        if (fireMode == FireMode.BasicGun || fireMode == FireMode.MachineGun || fireMode == FireMode.Machine2 || fireMode == FireMode.ShotGun || fireMode == FireMode.MissileGun || fireMode == FireMode.RandomGun)
        {
            gunAudio = GetComponent<AudioSource>();
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(projectile);
                obj.SetActive(false);
                bullets.Add(obj);
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        Lasertimer += Time.deltaTime;
        if (fireMode == FireMode.FireGun)
        {
            if (jsAim.isPressed || Input.GetMouseButtonDown(0))
            {
                isfire = true;
            }
            else if (jsAim.isPressed || Input.GetMouseButtonUp(0))
            {
                isfire = false;
            }

            if (isfire)
            {
                FireRange.SetActive(true);
                particle.Play();
            }
            else
            {
                FireRange.SetActive(false);
                particle.Stop();
            }
        }
    }
    

    void Shoot()
    {
        if (timer >= msBetweenShots && Time.timeScale != 0)
        {
            timer = 0f;
            /////////////////////////////LaserGun/////////////////////////////
            if (fireMode == FireMode.LaserGun && Lasertimer >= 1.7f)
            {
                Lasertimer = 0f;
                Invoke("LaserRangeOn", 1f);      
                particle.Play();
                laserparticle.Play();
                Invoke("shotOff", 1.5f);
            }
            /////////////////////////////LaserGun/////////////////////////////

            //////////////////////////////////////RandomGun////////////////////////////////////////////
            if (fireMode == FireMode.RandomGun)
            {
                    for (int j = 0; j < pooledAmount; j++)
                    {
                        if (!bullets[j].activeInHierarchy)
                        {
                            bullets[j].transform.position = bulletSpawn[Random.Range(0, 8)].position;
                            bullets[j].transform.rotation = bulletSpawn[Random.Range(0, 8)].rotation;
                            bullets[j].SetActive(true);
                            gunParticle.transform.position = bulletSpawn[Random.Range(0, 8)].position;
                            gunAudio.Stop();
                            gunAudio.Play();
                            gunParticle.Stop();
                            gunParticle.Play();
                            break;
                        }
                    }
                
            }
            //////////////////////////////////////RandomGun////////////////////////////////////////////

            //////////////////////////////////////machine2Gun////////////////////////////////////////////
            if (fireMode == FireMode.Machine2)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < pooledAmount; j++)
                    {
                        if (!bullets[j].activeInHierarchy)
                        {
                            bullets[j].transform.position = bulletSpawn[Random.Range(0, 8)].position;
                            bullets[j].transform.rotation = bulletSpawn[Random.Range(0, 8)].rotation;
                            bullets[j].SetActive(true);
                            gunParticle.transform.position = bulletSpawn[Random.Range(0, 8)].position;
                            gunAudio.Stop();
                            gunAudio.Play();
                            gunParticle.Stop();
                            gunParticle.Play();
                            break;
                        }
                    }
                }
            }
            //////////////////////////////////////machine2Gun////////////////////////////////////////////

            /////////////////////////////BasicGun, MachineGun, ShotGun, MissileGun/////////////////////////////
            if (fireMode == FireMode.BasicGun || fireMode == FireMode.MachineGun || fireMode == FireMode.ShotGun || fireMode == FireMode.MissileGun) { 
                for (int i = 0; i < bulletSpawn.Length; i++)
                {
                    for (int j = 0; j < pooledAmount; j++)
                    {
                        if (!bullets[j].activeInHierarchy)
                        {
                            bullets[j].transform.position = bulletSpawn[i].position;
                            bullets[j].transform.rotation = bulletSpawn[i].rotation;
                            bullets[j].SetActive(true);
                            gunAudio.Stop();
                            gunAudio.Play();                            
                            gunParticle.Stop();
                            gunParticle.Play();
                            break;
                        }
                    }
                }
            }
            /////////////////////////////BasicGun, MachinGun, ShotGun, MissileGun/////////////////////////////
        }
    }

    void shotOff()
    {
        Range.enabled = false;
        laserAudio.Stop();
        particle.Stop();
        laserparticle.Stop();
    }

    void LaserRangeOn()
    {
        laserAudio.Play();
        Range.enabled = true;
    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerRealeasedSinceLastShot = false;
    }

    public void OnTriggerRelease()
    {
        triggerRealeasedSinceLastShot = true;
    }
}
