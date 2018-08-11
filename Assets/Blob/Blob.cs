using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Blob : MonoBehaviour {

	//TODO: put it in different classes you dummy, just wish I had time for that! perhaps later? (famous last words of all spagDevs)
	protected Animator anim;
	protected Rigidbody2D rb;
	protected Collider2D col;
	protected SpriteRenderer sr;

	public int speed = 10;
	public bool jumping = true;
	public float jumpResetStart = 0.05f;
	public float jumpReset;
	public float maxVelocity;

	public Queue<Powers> powers;
	public int powerSlots = 3;

	public SlotTriggers[] powerUI;
	private int curSlot = 0;

	public GameObject deathPrefab;

	//Powers
	public float shiftStartTime;
	public float shiftTime;
	public bool shifting;

	public LayerMask groundLayer;

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Vector2 grav = Physics2D.gravity.normalized;
		float rotZ = Mathf.Atan2 (grav.y, grav.x) * Mathf.Rad2Deg;
		Vector3 t = Quaternion.Euler(0,0,90) * grav;

		Gizmos.DrawLine (transform.position, transform.position + (t.normalized));
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D> ();
		sr = GetComponent<SpriteRenderer> ();

		powers = new Queue<Powers> ();

		Debug.Log (Physics2D.gravity);
	}
	
	// Update is called once per frame
	void Update () {
		Controls ();
		MaintainPower ();
		LimitVeloticy ();

		if (!jumping) {
			float x = Input.GetAxis ("Horizontal");
			Vector3 vel = rb.velocity;
			Vector2 grav = Physics2D.gravity.normalized;
			float rotZ = Mathf.Atan2 (grav.y, grav.x) * Mathf.Rad2Deg;
			Vector3 t = Quaternion.Euler(0,0,90) * grav;
			//Orientate the player so movement feels better
				t.x = Mathf.Abs (t.x) * x;
				t.y = -Mathf.Abs (t.y) * x;
			Debug.Log ("Before:" + vel);
			vel.x = t.x != 0f ? (t.x * speed * Time.deltaTime * 50) : vel.x;
			vel.y = t.y != 0f ? (t.y * speed * Time.deltaTime * 50) : vel.y;
			Debug.Log (vel);
			rb.velocity = vel;
		}

		if(jumping)
			TryResetJump ();
	}

	void LimitVeloticy(){
		Vector3 vel = rb.velocity;
		vel.x = Mathf.Clamp (vel.x, -maxVelocity, maxVelocity);
		vel.y = Mathf.Clamp (vel.y, -maxVelocity, maxVelocity);
		rb.velocity = vel;
	}

	void Controls(){
		if(Input.GetMouseButtonDown(0)){
			if (jumping ) {
				if(powers.Count > 0 && powers.Peek () == Powers.JumpReset){
					UsePower (powers.Dequeue());
					Jump ();
				}
			} else {
				Jump ();
			} 
		}

		if (Input.GetMouseButtonDown (1) && powers.Count > 0) {
			Powers s = powers.Dequeue();
			UsePower (s);
		}
	}

	void Jump(){
		Debug.Log ("Jump");
		anim.SetTrigger("Jump");
		jumping = true;
		jumpReset = jumpResetStart;
		Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target.z = 0;
		Vector3 dir =  target - transform.position;
		dir = dir.normalized * speed;
		dir.z = 0;

		rb.velocity = dir;
	}

	void MaintainPower(){
		if (shifting) {
			if (shiftTime > 0) {
				shiftTime -= Time.deltaTime;
			} else {
				shifting = false;
				col.enabled = true;
				Color curColor = sr.color;
				curColor.r = 255;
				curColor.b = 255;
				sr.color = curColor;
			}
		}
	}

	// A power was dequeued to get here!
	void UsePower(Powers power){
		switch (power) {
		case Powers.Shift:
			shifting = true;
			shiftTime = shiftStartTime;
			col.enabled = false;
			// Change colours
			Color curColor = sr.color;
			curColor.r = 0;
			curColor.b = 0;
			sr.color = curColor;

			//Trigger the jump animation
			anim.SetTrigger("Jump");
			jumping = true;
			Debug.Log ("Shift power!");
			break;
		case Powers.JumpReset:
			jumping = false;
			break;
		}
		// A power was used, shift all powers one to the right
		for(int i = 0; i < 3; i++){
			if (powerUI [i].GetPos () > 0) {
				powerUI [i].SetPos (powerUI [i].GetPos () - 1);
			}
		}
	}


	void TryResetJump(){
		// Based on the gravity, reset your jump if you are no longer moving in the direction of the gravity;
		// Startign with four dimensional gravity, need to do some math once gravity can be in any direction

		Vector2 grav = Physics2D.gravity;

		if (Mathf.Abs(rb.velocity.x) < 0.1 && Mathf.Abs(rb.velocity.y) < 0.1) {
			// Also check in the direction of the gravity whether or not you are on a platform
			// The direction of the gravity is straight down (when gravity rotates, so does our blob)
			if (jumpReset <= 0) {
				jumping = false;
			} else {
				jumpReset -= Time.deltaTime;
			}
		}

		if (!jumping) {
			anim.SetTrigger ("Land");
		}
	}

	/**
	 * @Param power: A power to add to the list of available powers for the player
	 */ 
	public void AddPower(Powers power, Sprite powerSprite){
		if (powers.Count < powerSlots) {
			powers.Enqueue (power);
			Debug.Log (curSlot);
			powerUI [curSlot].SetSprite (powerSprite);
			int p = powers.Count;
			powerUI [curSlot].SetPos (p);
			curSlot = (curSlot+1) % 3;
		}
	}

	public void Destroy(){
		Instantiate (deathPrefab, transform.position, Quaternion.identity);
		rb.gravityScale = 0;
		rb.velocity = Vector3.zero;
		Invoke ("RestartLevel", 1);
	}

	void RestartLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
