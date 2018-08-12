using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectsAudioPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!MusicManager.playSoundEffects) {
			GetComponent<AudioSource> ().enabled = false;
			return;
		}

		Destroy (gameObject, 120f);
	}
}
