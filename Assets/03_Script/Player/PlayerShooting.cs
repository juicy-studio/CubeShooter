using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(GunController))]
public class PlayerShooting : MonoBehaviour {

    GunController gunController;
    public VirtualJSAim JsAim;

    // Use this for initialization
    void Start () {
        gunController = GetComponent<GunController> ();
	}
	
	// Update is called once per frame
	void Update () {
        // Weapon input
        if (JsAim.isPressed )
        {
            gunController.OnTriggerHold();
        }
        if (!JsAim.isPressed)
        {
            gunController.OnTriggerRelease();
        }
    }
}
