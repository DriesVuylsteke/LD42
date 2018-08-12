using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_Up : MonoBehaviour {

	public float speed;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = sr.size.x;
		float y = sr.size.y;
		y += speed * Time.deltaTime;
		sr.size = new Vector2(x,y);
	}
}
