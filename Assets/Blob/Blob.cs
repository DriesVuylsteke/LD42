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
	public float jumpResetStart = 0.3f;
	public float jumpReset;
	public float maxVelocity;

	public Queue<Powers> powers;
	public int powerSlots = 3;

	public SlotTriggers[] powerUI;
	private int curSlot = 0;

	public GameObject deathPrefab;
	public GameObject jumpPrefab;

	//Powers
	public float shiftStartTime;
	public float shiftTime;
	public bool shifting;

	public LayerMask groundLayer;

	public CameraFollow cam;
	public Vector3 lastVel;
	public float velForShake = 0.2f;

	public GameObject impactParticlePrefab;
	public GameObject dustSpawnGO;

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Vector2 grav = Physics2D.gravity.normalized;
		float rotZ = Mathf.Atan2 (grav.x, grav.y) * Mathf.Rad2Deg;
		Vector3 t = Quaternion.Euler(0,0,90) * grav;

		Gizmos.DrawLine (transform.position, transform.position + (t.normalized));

		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + new Vector3(grav.x, grav.y, 0));

		Gizmos.color = Color.green;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D> ();
		sr = GetComponent<SpriteRenderer> ();

		Physics2D.gravity = new Vector2 (0, -9.81f);

		powers = new Queue<Powers> ();

		lastVel = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		Controls ();
		MaintainPower ();
		LimitVeloticy ();

		// Determine impact or large velocity change
		if (Vector3.Distance (lastVel, rb.velocity) > velForShake && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.3) {
			cam.Shake (0.06f);
			Instantiate (impactParticlePrefab, dustSpawnGO.transform);
		}
		lastVel = rb.velocity;

		if (!jumping) {
			float x = Input.GetAxis ("Horizontal");
			Vector3 vel = rb.velocity.normalized;
			Vector2 grav = Physics2D.gravity.normalized;
			float rotZ = Mathf.Atan2 (grav.y, grav.x) * Mathf.Rad2Deg;

			RaycastHit2D hit = Physics2D.Raycast (dustSpawnGO.transform.position, grav, 1f);
			if (hit.collider == null) {
				jumping = true;
				jumpReset = jumpResetStart;
				anim.SetTrigger ("Jump");
			} else {

				Vector3 t = Quaternion.Euler (0, 0, 90) * grav;
				if (grav.x > grav.y) {
					//Orientate the player so movement feels better
					t.x = Mathf.Abs (t.x) * x;
					t.y = Mathf.Abs (t.y) * x;
				} else {
					t.x = Mathf.Abs (t.x) * x;
					t.y = -Mathf.Abs (t.y) * x;
				}
				vel.x = t.x != 0f ? (t.x * speed * Time.deltaTime * 50) : vel.x;
				vel.y = t.y != 0f ? (t.y * speed * Time.deltaTime * 50) : vel.y;
				rb.velocity = vel;
			}
		}

		if(jumping && !shifting)
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
		anim.SetTrigger("Jump");
		Instantiate (jumpPrefab, transform);
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

		if (Mathf.Abs(rb.velocity.x) < 0.15 && Mathf.Abs(rb.velocity.y) < 0.15) {
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
			Debug.Log ("Land");
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
