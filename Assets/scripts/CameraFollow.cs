using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Vector3 gravityOffset;

	void Start(){
		offset.x = 0;
		offset.y = 0;
	}

	void FixedUpdate(){
		if (target == null)
			return;
		Vector2 grav = Physics2D.gravity.normalized;
		gravityOffset = -grav.normalized * 2;

		Vector3 desiredPos = target.position + offset + gravityOffset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPos, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
