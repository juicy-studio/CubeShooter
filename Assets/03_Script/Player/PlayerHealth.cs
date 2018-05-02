using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float playerHealth = 100;
    public float currentHealth;
    public float flashSpeed = 5;
    public Slider healthSlider;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Image damageImage;
	public static float playerHealtPercent = 1.0f;
    public GameObject cameraTrans;
    AudioSource damageAudio;
    Animator cameraanim;
    Animator anim;
    bool damaged;
    bool invincible = true;
    public static bool isdead = false;

    void Awake()
    {
		isdead = false;
        currentHealth = playerHealth;
        healthSlider.value = currentHealth;
		healthSlider.maxValue = 100;
        anim = GetComponent<Animator>();
        cameraanim = cameraTrans.GetComponent<Animator>();
        damageAudio = GetComponent<AudioSource>();
    }
    
	void Update () {
        if (damaged)
        {
            damageImage.color = flashColor;
		//	Time.timeScale = 0.5f;
	//		Invoke ("ReturnTime", 0.5f);

        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;	
	}

    IEnumerator invincibleOn()
    {
        invincible = false;        
        anim.SetBool("Invincible", true);
        yield return new WaitForSeconds(2);
		anim.SetBool ("Invincible", false);
		invincible = true;
    }

    public void TakeDamage(float amount)
    {
        if (!isdead && invincible)
        {
            cameraanim.SetTrigger("CameraAnim");
            damaged = true;

            currentHealth -= amount;
            HealthManager.currentHealth = currentHealth;
            healthSlider.value = currentHealth;

            damageAudio.Stop();
            damageAudio.Play();

			if (currentHealth <= 0) {
				gameObject.SetActive (false);
				Invoke ("Death", 0.5f);
				isdead = true;
			} else {
				StartCoroutine ("invincibleOn");
			}
        }
    }

	public void ReturnTime()
	{
		Time.timeScale = 1;
	}

	public void Heal()
	{
		currentHealth = playerHealth;
        HealthManager.currentHealth = playerHealth;
		healthSlider.value = playerHealth;
	}

	public void HealtPercentUp()
	{
		playerHealth = playerHealth * playerHealtPercent;
		currentHealth = currentHealth * playerHealtPercent;
		healthSlider.maxValue = playerHealth;
        HealthManager.currentHealth = currentHealth;
        HealthManager.starthealth = playerHealth;
    }

	public void Death()
	{
		SceneManager.gameOver = true;
	}

}
