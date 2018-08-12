using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Vector3 gravityOffset;

	public Vector3 shakeOffset;
	public Vector3 initialShakePos;

	float pendingShakeDuration = 0f;
	bool isShaking;
	public float shakeAmount = 0.5f;

	void Start(){
		offset.x = 0;
		offset.y = 0;
	}

	void FixedUpdate(){
		if (target == null)
			return;
		if (pendingShakeDuration > 0 && !isShaking) {
			StartCoroutine (DoShake ());
		}


		Vector2 grav = Physics2D.gravity.normalized;
		gravityOffset = -grav.normalized * 2;

		Vector3 desiredPos = target.position + offset + gravityOffset + shakeOffset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPos, smoothSpeed);
		transform.position = smoothedPosition;
	}

	IEnumerator DoShake(){
		isShaking = true;

		var startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < startTime + pendingShakeDuration) {
			shakeOffset = new Vector3 (Random.Range (-shakeAmount, shakeAmount), Random.Range (-shakeAmount, shakeAmount), 0);
			yield return null;
		}

		pendingShakeDuration = 0f;
		shakeOffset = Vector3.zero;
		isShaking = false;
	}

	public void Shake(float duration){
		if (duration > 0) {
			pendingShakeDuration = duration;
		}
	}
}
