using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PowerCollect : MonoBehaviour {

	public Powers power;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Blob blob = other.gameObject.GetComponent<Blob> ();
			if (blob == null)
				Debug.LogError ("There exists a player without a blob script!");
			blob.AddPower (power, GetComponent<SpriteRenderer>().sprite);
			Destroy (gameObject);
		}
	}
}
