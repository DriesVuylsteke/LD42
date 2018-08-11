using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DangerTouch : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			Blob b = other.GetComponent<Blob> ();
			b.Destroy ();
		}
	}
}
