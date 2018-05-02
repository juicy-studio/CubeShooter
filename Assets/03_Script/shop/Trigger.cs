using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour {

    public bool shopActive;
    float StartTime;
    float second;
    bool IsPlayer;
	public GameObject ShopUi;

    public ParticleSystem DestroyEffect;



    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            OnShop();
            Time.timeScale = 0;
               IsPlayer = true;
        
        }

    }

   /* void OnTriggerExit(Collider col)
    {

    }*/

    void OnShop()
    {
		ShopUi.SetActive (true);

    }
    /*
    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += second * speed;
            Background.color = Color.Lerp(from, to, percent);
            yield return null;
        }

    }
    */

    public void Resume()
    {
      
        Time.timeScale = 1;
        // StartCoroutine(Fade(Color.black, Color.clear, 1));
		ShopUi.SetActive(false);
		if (IsPlayer == true)
		{
			Instantiate(DestroyEffect, transform.position, Quaternion.identity);
			Destroy(transform.parent.gameObject);
			IsPlayer = false;
			GameObject[] Bullets = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			foreach (GameObject Bullet in Bullets) {
			
				Bullet.SetActive (false);
			}

		
			//	Destroy (ShopUi);
		}
    }
		

}
