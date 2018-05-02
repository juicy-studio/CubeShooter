using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public static int skinNum = 0;
    public Material[] facebodySkin;

    Renderer body;
    Renderer face;
    void Awake()
    {
        if (PlayerPrefs.HasKey("skinNum"))
        {
            skinNum = PlayerPrefs.GetInt("skinNum");            
        }
        else
        {
            PlayerPrefs.SetInt("skinNum", skinNum);
        }
    }


    // Use this for initialization
    void Start()
    {
        body = GetComponent<MeshRenderer>();
        face = GameObject.Find("face").GetComponent<MeshRenderer>();

        face.material = facebodySkin[skinNum];
        body.material = facebodySkin[skinNum + 1];
    }
}
