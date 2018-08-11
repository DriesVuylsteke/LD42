using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SlotTriggers : MonoBehaviour {

	Animator anim;
	SpriteRenderer sr;
	public int pos = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	public void SetPos(int pos){
		anim.SetInteger ("slot", pos);
		this.pos = pos;
	}

	public void SetSprite(Sprite sprite){
		sr.sprite = sprite;
	}

	public Sprite GetSprite(){
		return sr.sprite;
	}

	public int GetPos(){
		return this.pos;
	}
}
