
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {

	public Text WeaponName;
	public Text WeaponPrice;
	public RawImage WeaponImage;
	public Button randBtt;
	public Button healthUpBtt;
	public Button fullHealthBtt;

	int num;

	[Range( 0,10)]
	public float RarePer = 8;

	private int Rare;

	public Gun[] Guns;
	public Gun[] RareGuns;
	GunController PlayerGun;
	Gun SellingGun;
	public Color RareColor;


	// Use this for initialization
	void Start () {
		PlayerGun = GameObject.Find ("Player").GetComponent<GunController> ();


		Rare = Random.Range (0, 11);

		if (RarePer < Rare) {
			num = Random.Range (0, RareGuns.Length);
			SellingGun = RareGuns [num];
		} else {
			num = Random.Range (0, Guns.Length);
			SellingGun = Guns [num];
		}

		while (PlayerGun.equippedGun == SellingGun) {
			if (RarePer < Rare) {
				num = Random.Range (0, RareGuns.Length);
				SellingGun = RareGuns [num];
			} else {
				num = Random.Range (0, Guns.Length);
				SellingGun = Guns [num];
			
			}	
		
		}

		if (RarePer < Rare) {
			WeaponPrice.text = SellingGun.GunPrice + "";
			WeaponImage.texture = SellingGun.GunImage;
	//		WeaponName.color = RareColor;
			WeaponName.text = SellingGun.GunName;
		
		}else{
			WeaponPrice.text = SellingGun.GunPrice + "";
			WeaponImage.texture = SellingGun.GunImage;
			WeaponName.text = SellingGun.GunName;
		}




		}





	public void BuyRandomWeapon()
	{
		if (RarePer < Rare) {
			if (PointRecord.Points >= RareGuns [num].GunPrice) {
				PointRecord.SpendPoint += RareGuns [num].GunPrice;
				PlayerGun.EquipGun (RareGuns [num]);
				randBtt.interactable = false;
			}
		} else {
			if (PointRecord.Points >= Guns [num].GunPrice) {
				PointRecord.SpendPoint += Guns [num].GunPrice;
				PlayerGun.EquipGun (Guns [num]);
				randBtt.interactable = false;
			}
		
		}
	}


	public void FullHealth()
	{
		if (PointRecord.Points >= 120) {
			PointRecord.SpendPoint += 120;
			GameObject.Find ("Player").GetComponent<PlayerHealth> ().Heal ();
			fullHealthBtt.interactable = false;

		}
	}
		

	public void HealthUp()
	{
		if (PointRecord.Points >= 60) {
			PointRecord.SpendPoint += 60;
			PlayerHealth.playerHealtPercent += 0.2f;
			GameObject.Find ("Player").GetComponent<PlayerHealth> ().HealtPercentUp ();
			healthUpBtt.interactable = false;
		}

	}



}
