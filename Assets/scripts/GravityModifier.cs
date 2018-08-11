using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityModifier : MonoBehaviour {

	public float verticalGravity;
	public float horizontalGravity;

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			Physics2D.gravity = new Vector2 (horizontalGravity, verticalGravity);
			Destroy (gameObject);

			Vector3 dir = new Vector3 (horizontalGravity, verticalGravity).normalized;
			float rotZ = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;

			other.transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);
		}
	}
}
