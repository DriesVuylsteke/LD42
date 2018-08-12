using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

	public static MusicManager instance = null;

	public static bool playMusic = true;
	public static bool playSoundEffects = true;
	public static bool showHelp = true;

	public Toggle musicButton;
	public Toggle soundButton;

	AudioSource music;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();
		music.enabled = playMusic;

		musicButton.isOn = playMusic;
		soundButton.isOn = playSoundEffects;
	}
	
	public void ToggleMusic(){
		playMusic = !playMusic;
		music.enabled = playMusic;
	}

	public void ToggleSoundEffects(){
		playSoundEffects = !playSoundEffects;
	}
}
