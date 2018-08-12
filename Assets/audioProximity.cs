using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioProximity : MonoBehaviour {

	AudioSource audio;
	Transform player;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (player.position, transform.position);
		dist = Mathf.Clamp (dist, 0, 3);
		audio.volume = Mathf.Lerp (0, 1, dist / 5f);
	}
}
