using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip[] musicArray;
	public AudioSource MusicObject;

	void Awake()
	{
			MusicObject = GetComponent<AudioSource> ();

		if (!MainUi.isMusicOn) {
			MusicObject.volume = 0;
		} else {
			MusicObject.clip = musicArray [Random.Range (0, musicArray.Length-1)];
			MusicObject.Play ();
			MusicObject.volume = 1;

		}
	}

	public void nextMusic(){
		MusicObject.clip = musicArray [Random.Range (0, musicArray.Length-1)];
		MusicObject.Play ();
	}

	public void SoundPlay()
	{
		if (!MainUi.isMusicOn) {
			MusicObject.volume = 0;
		} else {
			MusicObject.volume = 1;
		
		}


	}
}
